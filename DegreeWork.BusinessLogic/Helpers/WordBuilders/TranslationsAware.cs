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
    class TranslationsAware : IRecordAttributeTamper
    {
        public WordAttributes Attribute
        {
            get { return WordAttributes.Translations; }
        }

        public Expression<Func<DictionaryRecord, object>> IncludeExpression
        {
            get { return null; }
        }

        public object GetAttribute(DictionaryRecord record)
        {
            return record.Translations;
        }
    }
}
