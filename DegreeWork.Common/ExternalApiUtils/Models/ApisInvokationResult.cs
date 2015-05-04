using DegreeWork.Common.Entities;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ExternalApiUtils.Models
{
    public class ApisInvokationResult
    {
        private List<IApiResult> apiResults;
    
        public ApisInvokationResult(List<IApiResult> apiResults)
        {
            this.apiResults = apiResults;
        }

        public Word UpdateModel(Word word)
        {
            foreach(IApiResult result in apiResults)
                result.UpdateModel(word);

            return word;
        }
    }
}
