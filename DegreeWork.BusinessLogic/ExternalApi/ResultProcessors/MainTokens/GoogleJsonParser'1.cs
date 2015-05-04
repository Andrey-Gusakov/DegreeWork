using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ResultProcessors.MainTokens
{
    class GoogleJsonParser
    {
        private const int TOKENS_TOTAKE = 6;

        private JArray jArray;

        public GoogleJsonParser(string data)
        {
            jArray = JArray.Parse(data);
        }

        public string[] GetTokens<T>(IParseStrategy<T> strategy) where T : class
        {
            JArray[] chunks = GetChunks(strategy.Index);
            if(chunks == null || chunks.Length == 0)
                return new string[0];

            int left = strategy.TokensToTake;
            int portion = left / chunks.Length;

            List<T> rawTokens = new List<T>();
            for(int i = 0; i < chunks.Length - 1; i++)
            {
                JArray currentArray = chunks[i];
                int countToTake = currentArray.Count < portion ? currentArray.Count : portion;
                left = left - countToTake;
                if(countToTake < portion)
                {
                    countToTake = currentArray.Count;
                    portion = left / (chunks.Length - (i + 1));
                }

                rawTokens.AddRange(currentArray.Take(countToTake).Select(t => t as T));
            }
            rawTokens.AddRange(chunks[chunks.Length - 1].Take(left).Select(t => t as T));
            string[] result = strategy.GetTokens(rawTokens);
            return result;
        }

        private JArray[] GetChunks(int index)
        {
            JArray[] chunks = null;
            if(index < jArray.Count)
            {
                chunks = jArray[index]
                    .Cast<JArray>()
                    .Select(jarr => jarr[1])
                    .Cast<JArray>()
                    .OrderBy(jarr => jarr.Count)
                    .ToArray();
            }

            return chunks;
        }
    }
}
