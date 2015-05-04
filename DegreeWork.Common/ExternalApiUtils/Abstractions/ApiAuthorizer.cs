using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils.Abstractions
{
    public abstract class ApiAuthorizer : IApiRequestInterceptor
    {
        public abstract Task AuthorizeRequest(WebRequest request);

        public Task ModifyRequest(WebRequest request)
        {
            return AuthorizeRequest(request);
        }
    }
}
