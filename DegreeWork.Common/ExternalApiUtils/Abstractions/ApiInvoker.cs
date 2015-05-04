using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils.Abstractions
{
    public abstract class ApiInvoker
    {
        private IApiResultFactory apiResultFactory;

        public ApiInvoker(IApiResultFactory resultFactory)
        {
            this.apiResultFactory = resultFactory;
        }

        public abstract bool IsRequired { get; }

        public Task<IApiResult> InvokeAsync(string word)
        {
            WebRequest request = GetWebRequest(word);
            return PerformRequestAsync(request);
        }

        protected abstract WebRequest GetWebRequest(string word);

        protected async virtual Task<IApiResult> PerformRequestAsync(WebRequest request) 
        {
            WebResponse response = await request.GetResponseAsync();
            IApiResult apiResult;
            using(Stream responseStream = response.GetResponseStream())
                apiResult = await apiResultFactory.GetApiResultAsync(responseStream);

            return apiResult;
        }
    }
}
