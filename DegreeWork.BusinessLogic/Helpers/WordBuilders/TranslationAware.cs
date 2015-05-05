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
    class TranslationAware : IRecordAttributeTamper
    {
        public object GetAttribute(DictionaryRecord record)
        {
            int translationIndex = DateTime.Now.Day % record.Translations.Count;
            return record.Translations.ElementAt(translationIndex);
        }

        public WordAttributes Attribute
        {
            get { return WordAttributes.Translation; }
        }

        public Expression<Func<DictionaryRecord, object>> IncludeExpression
        {
            get { return d => d.Translations; }
        }
    }
}
