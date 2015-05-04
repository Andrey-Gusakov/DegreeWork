using DegreeWork.Common.Entities;
using DegreeWork.Common.ResourceManaging;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ResultProcessors
{
    class SpeechApiResult : IApiResult, IResourceHolder
    {
        private Task<StreamDescriptor> streamTask;
        private Stream stream;
        private string uri;

        public SpeechApiResult(Stream stream)
        {
            TaskCompletionSource<StreamDescriptor> taskSource = new TaskCompletionSource<StreamDescriptor>();
            taskSource.SetResult(new StreamDescriptor() {
                Stream = stream,
                Extension = "mp3"
            });
            this.stream = stream;
            this.streamTask = taskSource.Task;
        }

        #region IApiResult
        public bool HasPayload
        {
            get { return stream != null; }
        }

        public IResourceHolder GetResourceHolder()
        {
            return this;
        }

        public void UpdateModel(Word word)
        {
            word.PronuncationPath = uri;
        }
        #endregion


        #region IResourceHolder
        string IResourceHolder.ResourceKey
        {
            get { return "Pronunciations"; }
        }

        IEnumerable<Task<StreamDescriptor>> IResourceHolder.Streams
        {
            get { yield return streamTask; }
        }

        void IResourceHolder.SetPathTokens(string[] paths)
        {
            if(paths != null && paths.Length > 0)
                uri = paths[0].ToString();
        }
        #endregion
    }
}
