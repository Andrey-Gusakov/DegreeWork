using DegreeWork.BusinessLogic.ExternalApi.ApiResultFactories;
using DegreeWork.Common.ExternalApiUtils.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ApiInvokers
{
    class MainTokensApiInvoker : ApiInvoker
    {
        private const string MAINRESOURCES_PATTERN = "https://translate.google.com/translate_a/single?client=t&sl=en&tl=ru&hl=en&dt=bd&dt=ex&dt=ld&dt=md&dt=qc&dt=rw&dt=rm&dt=ss&dt=t&dt=at&ie=UTF-8&oe=UTF-8&source=bh&ssel=0&tsel=0&kc=1&tk=519666|291861&q={0}";

        public MainTokensApiInvoker(MainTokensApiResultFactory mainTokensFactory) : base(mainTokensFactory) { }

        public override bool IsRequired
        {
            get { return true; }
        }

        protected override WebRequest GetWebRequest(string word)
        {
            WebRequest request = WebRequest.Create(String.Format(MAINRESOURCES_PATTERN, Uri.EscapeDataString(word)));
            return request;
        }
    }
}
