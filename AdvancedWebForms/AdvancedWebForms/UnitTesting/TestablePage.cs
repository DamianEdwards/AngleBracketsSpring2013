using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AdvancedWebForms.UnitTesting
{
    public abstract class TestablePage : System.Web.UI.Page
    {
        private HttpContextBase _httpContextBase;
        private HttpRequestBase _httpRequestBase;
        private HttpResponseBase _httpResponseBase;
        private HttpSessionStateBase _httpSessionStateBase;
        private HttpApplicationStateBase _httpApplicationStateBase;

        public TestablePage(HttpContextBase httpContext, HttpRequestBase httpRequest, HttpResponseBase httpResponse, HttpSessionStateBase httpSessionState, HttpApplicationStateBase httpApplicationState)
        {
            _httpContextBase = httpContext;
            _httpRequestBase = httpRequest;
            _httpResponseBase = httpResponse;
            _httpSessionStateBase = httpSessionState;
            _httpApplicationStateBase = httpApplicationState;
        }

        protected HttpContextBase HttpContextBase
        {
            get
            {
                if (_httpContextBase == null)
                {
                    _httpContextBase = new HttpContextWrapper(Context);
                }
                return _httpContextBase;
            }
        }

        protected HttpRequestBase HttpRequestBase
        {
            get
            {
                if (_httpRequestBase == null)
                {
                    _httpRequestBase = new HttpRequestWrapper(Request);
                }
                return _httpRequestBase;
            }
        }

        protected HttpResponseBase HttpResponseBase
        {
            get
            {
                if (_httpResponseBase == null)
                {
                    _httpResponseBase = new HttpResponseWrapper(Response);
                }
                return _httpResponseBase;
            }
        }

        protected HttpSessionStateBase HttpSessionStateBase
        {
            get
            {
                if (_httpSessionStateBase == null)
                {
                    _httpSessionStateBase = new HttpSessionStateWrapper(Session);
                }
                return _httpSessionStateBase;
            }
        }

        protected HttpApplicationStateBase HttpApplicationStateBase
        {
            get
            {
                if (_httpApplicationStateBase == null)
                {
                    _httpApplicationStateBase = new HttpApplicationStateWrapper(Application);
                }
                return _httpApplicationStateBase;
            }
        }
    }
}