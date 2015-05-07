using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

        public string[] FallbackGet(JArray array)
        {
            while(array.Count > 0 && array[0] is JArray)
                array = array[0] as JArray;

            string[] result = null;
            if(array.Count > 0)
            {
                string token = array[0].ToString();
                if(Regex.IsMatch(token, @"^[\p{IsCyrillic}\s]+$"))
                    result = new string[] { token };
            }
            
            return result;
        }
    }
}
