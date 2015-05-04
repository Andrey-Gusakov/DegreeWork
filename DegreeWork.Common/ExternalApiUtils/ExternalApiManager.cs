using DegreeWork.Common.Entities;
using DegreeWork.Common.ExternalApiUtils.Abstractions;
using DegreeWork.Common.ExternalApiUtils.Models;
using DegreeWork.Common.ResourceManaging;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils
{
    public class ExternalApisManager
    {
        private ResourceManager resourceManager;
        private IEnumerable<ApiInvoker> invokers;

        public ExternalApisManager(ResourceManager resourceManager, IEnumerable<ApiInvoker> invokers)
        {
            this.invokers = invokers;
            this.resourceManager = resourceManager;
        }

        public async Task<ApisInvokationResult> CollectResources(string term)
        {
            var groupedInvokers = invokers.GroupBy(invoker => invoker.IsRequired);

            IApiResult[] requiredResult = await GetInvokersResult(groupedInvokers.FirstOrDefault(gr => gr.Key), term);
            if(requiredResult.Any(r => !r.HasPayload))
                return null;

            IApiResult[] restResult = await GetInvokersResult(groupedInvokers.FirstOrDefault(gr => !gr.Key), term);

            List<IApiResult> apisResults = new List<IApiResult>(requiredResult.Union(restResult));
            await resourceManager.AllocateResources(term, apisResults);

            ApisInvokationResult result = new ApisInvokationResult(apisResults);
            return result;
        }

        private Task<IApiResult[]> GetInvokersResult(IEnumerable<ApiInvoker> invokers, string term)
        {
            List<Task<IApiResult>> tasks = new List<Task<IApiResult>>();
            foreach(var invoker in invokers)
            {
                Task<IApiResult> deferredResult = invoker.InvokeAsync(term);
                tasks.Add(deferredResult);
            }

            return Task.WhenAll(tasks);
        }
    }
}
