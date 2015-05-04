using DegreeWork.BusinessLogic.ExternalApi.ApiResultFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ApiInvokers
{
    class SpeechProviderDataMarketApiInvoker : OAuthDataMarketApiInvoker
    {
        private const string SPEECH_RESOURCE_PATTERN = "http://api.microsofttranslator.com/v2/Http.svc/Speak?text={0}&language=en&format={1}&options=MaxQuality";
        public SpeechProviderDataMarketApiInvoker(SpeechApiResultFactory speechFactory)
            : base(speechFactory) { }

        public override bool IsRequired
        {
            get { return false; }
        }

        protected override WebRequest GetWebRequest(string word)
        {
            WebRequest request = WebRequest.Create(String.Format(
                SPEECH_RESOURCE_PATTERN,
                Uri.EscapeDataString(word),
                Uri.EscapeDataString("audio/mp3"))
            );

            return request;
        }
    }
}
