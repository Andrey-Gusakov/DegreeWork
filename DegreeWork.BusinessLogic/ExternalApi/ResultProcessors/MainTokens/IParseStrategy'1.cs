using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.ExternalApi.ResultProcessors.MainTokens
{
    internal interface IParseStrategy<T> where T : class
    {
        int TokensToTake { get; }
        int Index { get; }
        string[] GetTokens(List<T> rawTokens);
        string[] FallbackGet(JArray array);
    }
}
