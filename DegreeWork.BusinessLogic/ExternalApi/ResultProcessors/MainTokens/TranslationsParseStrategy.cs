using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ResultProcessors.MainTokens
{
    class TranslationsParseStrategy : IParseStrategy<JToken>
    {
        private const int TOKENS_TOTAKE = 6;
        private const int RESOURCE_INDEX = 1;

        public int TokensToTake
        {
            get { return TOKENS_TOTAKE; }
        }

        public int Index
        {
            get { return RESOURCE_INDEX; }
        }

        public string[] GetTokens(List<JToken> rawTokens)
        {
            return rawTokens.Select(t => t.ToString()).ToArray();
        }
    }
}
