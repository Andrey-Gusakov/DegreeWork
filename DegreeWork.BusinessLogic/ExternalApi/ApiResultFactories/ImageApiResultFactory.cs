﻿using DegreeWork.BusinessLogic.ExternalApi.ResultProcessors;
using DegreeWork.Common.ExternalApiUtils.ApiResultFactories;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ApiResultFactories
{
    class ImageApiResultFactory : StringReaderApiResultFactory
    {
        protected override IApiResult InstantiateObject(string data)
        {
            return new ImageApiResult(data);
        }
    }
}
