using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ResultProcessors.MainTokens
{
    class MainTokensApiResult : IApiResult
    {
        private string[] translations;
        private string[] thesauruses;

        public MainTokensApiResult(string data)
        {
            GoogleJsonParser parser = new GoogleJsonParser(data);
            this.translations = parser.GetTokens(new TranslationsParseStrategy());
            this.thesauruses = parser.GetTokens(new ThesaurusesParseStrategy());
        }

        public bool HasPayload
        {
            get { return translations.Length > 0; }
        }

        public IResourceHolder GetResourceHolder()
        {
            return null;
        }

        public void UpdateModel(Word word)
        {
            word.Translations = translations
                .Select(t => new Translation() { Representation = t })
                .ToArray();

            word.Thesauruses = thesauruses
                .Select(t => new Thesaurus() { Definition = t })
                .ToArray();
        }
    }
}
