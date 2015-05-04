using DegreeWork.Common.ResourceManaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ResourceProcessing.Abstractions
{
    public interface IResourceHolder
    {
        string ResourceKey { get; }
        IEnumerable<Task<StreamDescriptor>> Streams { get; }
        void SetPathTokens(string[] paths);
    }
}
