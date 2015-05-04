using DegreeWork.Common.ExternalApiUtils.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils
{
    abstract class BodyBuilder: IApiRequestInterceptor
    {
        protected string body;

        public BodyBuilder(string body)
        {
            this.body = body;
        }

        public async Task ModifyRequest(WebRequest request)
        {
            request.Method = "POST";
            Stream requestStream = await request.GetRequestStreamAsync();
            using(requestStream)
            {
                byte[] bytes = Encoding.ASCII.GetBytes(body);
                await requestStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}
