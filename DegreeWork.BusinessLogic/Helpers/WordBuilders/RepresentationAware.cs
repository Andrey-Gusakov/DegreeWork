﻿using DegreeWork.Common.Entities;
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
    class RepresentationAware : IRecordAttributeTamper
    {
        public WordAttributes Attribute
        {
            get { return WordAttributes.Representation; }
        }

        public Expression<Func<DictionaryRecord, object>> IncludeExpression
        {
            get { return d => d.Word; }
        }

        public object GetAttribute(DictionaryRecord record)
        {
            return record.Word.Representation;
        }
    }
}
