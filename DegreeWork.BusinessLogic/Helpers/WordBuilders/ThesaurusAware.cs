using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers.WordBuilders
{
    class ThesaurusAware : IRecordAttributeTamper
    {
        public WordAttributes Attribute
        {
            get { return WordAttributes.Thesaurus; }
        }

        public Expression<Func<DictionaryRecord, object>> IncludeExpression
        {
            get { return d => d.Thesaurus; }
        }

        public object GetAttribute(DictionaryRecord record)
        {
 	        return record.Thesaurus.Definition;
        }
}
}
