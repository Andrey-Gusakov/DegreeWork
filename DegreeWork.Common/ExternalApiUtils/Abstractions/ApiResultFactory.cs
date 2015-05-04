using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils.Abstractions
{
    public interface IApiResultFactory
    {
        Task<IApiResult> GetApiResultAsync(Stream stream);
    }
}
