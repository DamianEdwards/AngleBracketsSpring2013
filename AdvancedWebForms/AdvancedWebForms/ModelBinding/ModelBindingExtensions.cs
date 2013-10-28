using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.ModelBinding;

namespace AdvancedWebForms.ModelBinding
{
    public static class ModelBindingExtensions
    {
        public static dynamic BindFromForm<T>(this ModelBindingExecutionContext context, T model) where T : class
        {
            var valueProvider = new FormValueProvider(context);
            return BindFromValueProvider(context, model, valueProvider);
        }

        public static dynamic BindFromQueryString<T>(this ModelBindingExecutionContext context, T model) where T : class
        {
            var valueProvider = new QueryStringValueProvider(context);
            return BindFromValueProvider(context, model, valueProvider);
        }

        public static dynamic BindFromQueryString<T>(this ModelBindingExecutionContext context) where T : class, new()
        {
            var valueProvider = new QueryStringValueProvider(context);
            return BindFromValueProvider<T>(context, valueProvider);
        }

        public static dynamic Bind<T>(this ModelBindingExecutionContext context, T model) where T : class
        {
            var valueProvider = new AggregateValueProvider(context);
            return BindFromValueProvider(context, model, valueProvider);
        }

        public static T BindFromValueProvider<T>(this ModelBindingExecutionContext context, IValueProvider valueProvider) where T : class, new()
        {
            var binder = ModelBinders.Binders.DefaultBinder;
            var model = new T();

            var bindingContext = new ModelBindingContext()
            {
                ModelBinderProviders = ModelBinderProviders.Providers,
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType()),
                ModelState = context.ModelState,
                ValueProvider = valueProvider
            };

            binder.BindModel(context, bindingContext);
            return (T)bindingContext.Model;
        }

        public static dynamic BindFromValueProvider<T>(this ModelBindingExecutionContext context, T model, IValueProvider valueProvider) where T : class
        {
            var binder = ModelBinders.Binders.DefaultBinder;

            var bindingContext = new ModelBindingContext()
            {
                ModelBinderProviders = ModelBinderProviders.Providers,
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, model.GetType()),
                ModelState = context.ModelState,
                ValueProvider = valueProvider
            };

            binder.BindModel(context, bindingContext);
            return new DynamicModel(bindingContext.Model);
        }
    }

    public class AggregateValueProvider : IValueProvider, IUnvalidatedValueProvider
    {
        private readonly List<IUnvalidatedValueProvider> _valueProviders = new List<IUnvalidatedValueProvider>();

        public AggregateValueProvider(ModelBindingExecutionContext modelBindingExecutionContext)
        {
            _valueProviders.Add(new QueryStringValueProvider(modelBindingExecutionContext));
            _valueProviders.Add(new CookieValueProvider(modelBindingExecutionContext));
            _valueProviders.Add(new FormValueProvider(modelBindingExecutionContext));
        }

        public bool ContainsPrefix(string prefix)
        {
            return _valueProviders.Any(vp => vp.ContainsPrefix(prefix));
        }

        public ValueProviderResult GetValue(string key)
        {
            return GetValue(key, false);
        }

        public ValueProviderResult GetValue(string key, bool skipValidation)
        {
            return _valueProviders.Select(vp => vp.GetValue(key, skipValidation))
                .LastOrDefault();
        }
    }

    public class ImmutableObjectModelBinderProvider : ModelBinderProvider
    {
        public override IModelBinder GetBinder(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
        {
            if (!bindingContext.ValueProvider.ContainsPrefix(bindingContext.ModelName))
            {
                // the value provider can't give us anything for this model
                return null;
            }

            // there must be a single public constructor
            var candidateCtors = bindingContext.ModelType.GetConstructors(BindingFlags.Public | BindingFlags.Instance);
            if (candidateCtors.Length != 1)
            {
                return null;
            }

            // the constructor must be parameterful
            var ctor = candidateCtors[0];
            var ctorParameters = ctor.GetParameters();
            if (ctorParameters.Length == 0)
            {
                return null;
            }

            // all constructor parameters must have corresponding property metadatas
            foreach (ParameterInfo pInfo in ctorParameters)
            {
                ModelMetadata propertyMetadata;
                bindingContext.PropertyMetadata.TryGetValue(pInfo.Name, out propertyMetadata);

                if (propertyMetadata == null || propertyMetadata.ModelType != pInfo.ParameterType)
                {
                    return null; // property not found or type didn't match
                }
            }

            // all checks passed
            return new ImmutableObjectModelBinder();
        }
    }

    public class ImmutableObjectModelBinder : IModelBinder
    {
        public bool BindModel(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext)
        {
            // Provider ensures that there's exactly one constructor and that
            // its parameters have corresponding property metadatas.
            var propertyMetadatas = bindingContext.ModelMetadata.Properties;
            var dto = CreateAndPopulateDto(modelBindingExecutionContext, bindingContext, propertyMetadatas);
            ProcessDto(modelBindingExecutionContext, bindingContext, dto);

            return true; // tried to set a model, and validation should run
        }

        private ComplexModel CreateAndPopulateDto(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, IEnumerable<ModelMetadata> propertyMetadatas)
        {
            var originalDto = new ComplexModel(bindingContext.ModelMetadata, propertyMetadatas);

            // The binding context's copy constructor clones many useful properties
            // such as the value provider, provider collection, ModelState, and
            // others. We only set the properties that must be changed.
            var newMBC = new ModelBindingContext(bindingContext)
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => originalDto, typeof(ComplexModel)),
                ModelName = bindingContext.ModelName
            };

            // Get the binder that can handle our DTO, then call into it to retrieve
            // values for this model's properties. The inner binder won't set the
            // properties directly, but it will populate the DTO with the appropriate
            // values so that we can read them later.
            var binder = ModelBinderProviders.Providers.GetBinder(modelBindingExecutionContext, newMBC);
            binder.BindModel(modelBindingExecutionContext, newMBC);
            return (ComplexModel)newMBC.Model;
        }

        private void ProcessDto(ModelBindingExecutionContext modelBindingExecutionContext, ModelBindingContext bindingContext, ComplexModel dto)
        {
            var ctorParameterValues = new List<object>();

            // need to retrieve the value for each property from the DTO
            foreach (var propertyMetadata in dto.PropertyMetadata)
            {
                ComplexModelResult dtoResult;
                dto.Results.TryGetValue(propertyMetadata, out dtoResult);

                object currentValue = null;
                if (dtoResult != null)
                {
                    currentValue = dtoResult.Model;
                    // need to merge each of the child validation nodes up
                    bindingContext.ValidationNode.ChildNodes.Add(dtoResult.ValidationNode);
                }

                ctorParameterValues.Add(currentValue);
            }

            // finally, instantiate the model
            object newModel = null;
            try
            {
                newModel = Activator.CreateInstance(bindingContext.ModelType, ctorParameterValues.ToArray());
            }
            catch (Exception ex)
            {
                // record error in ModelState
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, ex);
            }

            bindingContext.Model = newModel;
            // validate all properties, even those we didn't get values for
            bindingContext.ValidationNode.ValidateAllProperties = true;
        }
    }

    public class DynamicModel : DynamicObject
    {
        private readonly object _model;
        private readonly Type _modelType;

        public DynamicModel(object model)
        {
            if (model == null) return;

            _model = model;
            _modelType = _model.GetType();
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return _modelType.GetMembers().Select(mi => mi.Name);
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = "";
            if (_modelType == null) return true;

            var mi = _modelType.GetMember(binder.Name).FirstOrDefault();

            if (mi != null)
            {
                var pi = mi as PropertyInfo;
                if (pi == null) return true;

                result = pi.GetValue(_model);
                return true;
            }

            return true;
        }
    }
}