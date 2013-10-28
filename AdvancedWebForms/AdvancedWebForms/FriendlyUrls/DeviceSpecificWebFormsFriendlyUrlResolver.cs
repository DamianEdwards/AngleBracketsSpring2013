using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Microsoft.AspNet.FriendlyUrls.Resolvers;

namespace AdvancedWebForms.FriendlyUrls
{
    public class DeviceSpecificWebFormsFriendlyUrlResolver : WebFormsFriendlyUrlResolver
    {
        private readonly IDictionary<string, string> _deviceUserAgentMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Opera Mobi", "OperaMobile" },
            { "iPhone", "iPhone" },
            { "iPad", "iPad" }
        };

        public override IList<string> GetExtensions(HttpContextBase httpContext)
        {
            var extensions = base.GetExtensions(httpContext).ToList();
            if (extensions.Any(e => base.IsMobileExtension(httpContext, e)))
            {
                // Base has determined we should look for a mobile page, let's add device specific
                // extension to the beginning.
                var deviceSpecificSufffix = GetDeviceSpecificSuffix(httpContext);
                if (!String.IsNullOrEmpty(deviceSpecificSufffix))
                {
                    extensions.Insert(0, "." + deviceSpecificSufffix + ".aspx");
                }
            }
            return extensions;
        }

        protected override bool IsMobileExtension(HttpContextBase httpContext, string extension)
        {
            return base.IsMobileExtension(httpContext, extension) ||
                _deviceUserAgentMap.Values.Any(v => extension.IndexOf(v, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        protected override bool TrySetMobileMasterPage(HttpContextBase httpContext, Page page, string mobileSuffix)
        {
            var deviceSpecificSuffix = GetDeviceSpecificSuffix(httpContext);
            if (!String.IsNullOrEmpty(deviceSpecificSuffix) && base.TrySetMobileMasterPage(httpContext, page, deviceSpecificSuffix))
            {
                // We were able to set a device specific master page, so just return
                return true;
            }

            // Just use the base logic
            return base.TrySetMobileMasterPage(httpContext, page, mobileSuffix);
        }

        private string GetDeviceSpecificSuffix(HttpContextBase httpContext)
        {
            foreach (var item in _deviceUserAgentMap)
            {
                if (httpContext.Request.UserAgent.IndexOf(item.Key, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    return item.Value;
                }
            }

            return String.Empty;
        }
    }
}