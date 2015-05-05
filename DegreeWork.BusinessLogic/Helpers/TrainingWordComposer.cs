using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers
{
    class TrainingWordComposer
    {
        private readonly IEnumerable<IRecordAttributeAware> informators;

        public TrainingWordComposer(IEnumerable<IRecordAttributeAware> informators)
        {
            this.informators = informators;
        }

        public Dictionary<string, object>[] ComposeWordsArray(int trainigId, 
            IDbRequestMetainfo metainfo, WordAttributes[] attributes)
        {
            Expression<Func<
            foreach(WordAttributes attribute in attributes)
            {

            }
        }
    }
}
