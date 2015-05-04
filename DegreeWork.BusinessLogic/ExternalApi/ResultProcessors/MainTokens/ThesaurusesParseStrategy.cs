using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ResultProcessors.MainTokens
{
    class ThesaurusesParseStrategy : IParseStrategy<JArray>
    {
        private const int TOKENS_TOTAKE = 6;
        private const int RESOURCE_INDEX = 12;

        public int TokensToTake
        {
            get { return TOKENS_TOTAKE; }
        }

        public int Index
        {
            get { return RESOURCE_INDEX; }
        }

        public string[] GetTokens(List<JArray> rawTokens)
        {
            return rawTokens.Select(jarr => (string)jarr[0]).ToArray();
        }
    }
}
