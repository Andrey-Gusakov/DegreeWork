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
    public abstract class StreamConverterApiResultFactory : IApiResultFactory
    {
        private const int STREAM_CAPACITY = 1024;

        public async Task<IApiResult> GetApiResultAsync(Stream stream)
        {
            MemoryStream memoryStream = new MemoryStream(STREAM_CAPACITY);
            await stream.CopyToAsync(memoryStream);
            memoryStream.Seek(0, SeekOrigin.Begin);
            IApiResult result = InstantiateObject(memoryStream);
            return result;
        }

        protected abstract IApiResult InstantiateObject(Stream memoryStream);
    }
}
