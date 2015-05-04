using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ResultProcessors
{
    class ImageApiResult : IApiResult
    {
        private string[] externalUris;
        private UrisContext context;

        public ImageApiResult(string data)
        {
            ExtractUris(data);
        }

        private void ExtractUris(string data)
        {
            JObject json = JObject.Parse(data);
            externalUris = json
                .SelectToken("d.results")
                .Children()
                .Select(t => (string)t.SelectToken("MediaUrl"))
                .ToArray();
        }

        public bool HasPayload
        {
            get { return externalUris.Length > 0; }
        }

        public IResourceHolder GetResourceHolder()
        {
            context = new UrisContext();
            return new ImageResourceHolder(context, externalUris);
        }

        public void UpdateModel(Word word)
        {
            if(context == null || context.Uris == null)
                return;

            word.WordImages = context.Uris
                .Select(s => new WordImage() { ImagePath = s })
                .ToArray();
        }
    }
}
