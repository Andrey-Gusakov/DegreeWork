using DegreeWork.BusinessLogic.ExternalApi;
using DegreeWork.Common.ExternalApiUtils.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ApiInvokers
{
    abstract class OAuthDataMarketApiInvoker : InterceptableApiInvoker
    {
        public OAuthDataMarketApiInvoker(IApiResultFactory factory, params IApiRequestInterceptor[] interceptors)
            : base(factory,
                new List<IApiRequestInterceptor> { MicrosoftOAuthDataMarketAuthorizer.Instanse }
                    .Union(interceptors ?? new IApiRequestInterceptor[0])
                    .ToArray()
            )
        {
        }
    }
}
