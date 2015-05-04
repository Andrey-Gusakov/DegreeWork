using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils.Abstractions
{
    public abstract class InterceptableApiInvoker : ApiInvoker
    {
        private IApiRequestInterceptor[] requestInterceptors;

        public InterceptableApiInvoker(IApiResultFactory factory, params IApiRequestInterceptor[] interceptors)
            : base(factory)
        {
            this.requestInterceptors = interceptors;
        }

        protected override async Task<IApiResult> PerformRequestAsync(WebRequest request)
        {
            if(requestInterceptors != null)
            {
                foreach(IApiRequestInterceptor interceptor in requestInterceptors)
                    await interceptor.ModifyRequest(request);
            }

            IApiResult result = await base.PerformRequestAsync(request);
            return result;
        }
    }
}
