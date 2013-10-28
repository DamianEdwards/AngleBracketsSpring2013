using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace AdvancedWebForms.Async
{
    public static class HttpResponseExtensions
    {
        public static Task FlushAsync(this HttpResponse response)
        {
            if (response.SupportsAsyncFlush)
            {
                return Task.Factory.FromAsync((cb, state) => response.BeginFlush(cb, state), iar => response.EndFlush(iar), null);
            }

            response.Flush();
            return Task.FromResult(0);
        }
    }
}