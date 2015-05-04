using DegreeWork.Common.ResourceManaging;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ResultProcessors
{
    class ImageResourceHolder : IResourceHolder
    {
        private UrisContext context;
        private string[] externalUris;

        public ImageResourceHolder(UrisContext context, string[] externalUris)
        {
            this.context = context;
            this.externalUris = externalUris;
        }

        public string ResourceKey
        {
            get { return "Images"; }
        }

        public IEnumerable<Task<StreamDescriptor>> Streams
        {
            get
            {
                foreach(string imageUrl in externalUris)
                {
                    Task<StreamDescriptor> result = Task.Run<StreamDescriptor>(async () => {
                        WebRequest request = WebRequest.Create(imageUrl);
                        HttpWebResponse response = null;
                        try
                        {
                            response = await request.GetResponseAsync() as HttpWebResponse;
                        }
                        catch(WebException)
                        {
                            response = null;
                        }
                        Stream stream = response != null && response.StatusCode == HttpStatusCode.OK ? 
                            response.GetResponseStream() : null;

                        return new StreamDescriptor() {
                            Stream = stream,
                            Extension = "jpg"
                        };
                    });

                    yield return result;
                }
            }
        }

        public void SetPathTokens(string[] paths)
        {
            context.Uris = paths;
        }
    }
}
