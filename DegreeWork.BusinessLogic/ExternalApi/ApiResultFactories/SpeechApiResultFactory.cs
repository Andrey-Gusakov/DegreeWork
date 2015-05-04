using DegreeWork.BusinessLogic.ExternalApi.ResultProcessors;
using DegreeWork.Common.ExternalApiUtils.ApiResultFactories;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ApiResultFactories
{
    class SpeechApiResultFactory : StreamConverterApiResultFactory
    {
        protected override IApiResult InstantiateObject(Stream memoryStream)
        {
            return new SpeechApiResult(memoryStream);
        }
    }
}
