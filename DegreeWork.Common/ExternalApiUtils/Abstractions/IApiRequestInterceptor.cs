using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils.Abstractions
{
    public interface IApiRequestInterceptor
    {
        Task ModifyRequest(WebRequest request);
    }
}
