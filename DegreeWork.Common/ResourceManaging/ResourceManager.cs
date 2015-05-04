using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ResourceManaging
{
    public class ResourceManager
    {
        private readonly IResourceAllocator allocator;

        public ResourceManager(IResourceAllocator allocator)
        {
            this.allocator = allocator;
        }

        public async Task AllocateResources(string filePrefix, List<IApiResult> apiResults)
        {
            foreach(IApiResult apiResult in apiResults)
            {
                IResourceHolder holder = apiResult.GetResourceHolder();
                if(holder != null)
                {
                    List<string> uris = new List<string>();
                    foreach(Task<StreamDescriptor> streamTask in holder.Streams)
                    {
                        StreamDescriptor streamDescriptor = await streamTask;
                        if(streamDescriptor.Stream != null)
                        {
                            using(streamDescriptor.Stream)
                            {
                                string uri = await allocator.SaveAsync(holder.ResourceKey, streamDescriptor, filePrefix);
                                uris.Add(uri);
                            }
                        }
                    }

                    holder.SetPathTokens(uris.ToArray());
                }
            }
        }
    }
}
