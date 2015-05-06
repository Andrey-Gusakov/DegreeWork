using Autofac.Features.Indexed;
using DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting;
using DegreeWork.Common;
using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers
{
    class TrainingWordComposer
    {
        private readonly IEnumerable<IRecordAttributeTamper> tampers;

        public TrainingWordComposer(IEnumerable<IRecordAttributeTamper> tampers)
        {
            this.tampers = tampers;
        }

        public Dictionary<string, object>[] ComposeWordsArray(WordAttributes[] attributes,
            Func<Expression<Func<DictionaryRecord, object>>[], List<DictionaryRecord>> wordsGetter)
        {
            IRecordAttributeTamper[] activeTampers = attributes.Select(attr => tampers.First(worker => worker.Attribute == attr)).ToArray();
            Expression<Func<DictionaryRecord, object>>[] includes = activeTampers
                .Select(i => i.IncludeExpression)
                .Where(expr => expr != null)
                .ToArray();

            List<DictionaryRecord> records = wordsGetter(includes);
            Dictionary<string, object>[] result = Enumerable
                .Repeat<Func<Dictionary<string, object>>>(() => new Dictionary<string, object>(), records.Count)
                .Select(f => f())
                .ToArray();

            foreach(IRecordAttributeTamper tamper in activeTampers)
            {
                string key = tamper.Attribute.ToString().ToLower();
                for(int i = 0; i < records.Count; i++)
                    result[i][key] = tamper.GetAttribute(records[i]);
            }

            return result;
        }
    }
}
