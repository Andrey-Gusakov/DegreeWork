using DegreeWork.Common.ExternalApiUtils.Abstractions;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils.ApiResultFactories
{
    public abstract class StringReaderApiResultFactory : IApiResultFactory
    {
        public async Task<IApiResult> GetApiResultAsync(Stream stream)
        {
            string data;
            using(StreamReader reader = new StreamReader(stream))
                data = await reader.ReadToEndAsync();

            IApiResult result = InstantiateObject(data);
            return result;
        }

        protected abstract IApiResult InstantiateObject(string data);
    }
}
