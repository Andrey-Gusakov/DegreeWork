using DegreeWork.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ResourceProcessing.Abstractions
{
    public interface IApiResult
    {
        bool HasPayload { get; }
        IResourceHolder GetResourceHolder();
        void UpdateModel(Word word);
    }
}
