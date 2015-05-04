using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ResourceManaging
{
    public interface IResourceAllocator
    {
        Task<string> SaveAsync(string key, StreamDescriptor streamDescriptor, string filePrefix = null);
    }
}
