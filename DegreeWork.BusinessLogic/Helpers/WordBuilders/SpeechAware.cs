﻿using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.ResourceManaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers.WordBuilders
{
    class SpeechAware : IRecordAttributeTamper
    {
        private IPathResolver pathResolver;

        public SpeechAware(IPathResolver pathResolver)
        {
            this.pathResolver = pathResolver;
        }

        public WordAttributes Attribute
        {
            get { return WordAttributes.Pronunciation; }
        }

        public Expression<Func<DictionaryRecord, object>> IncludeExpression
        {
            get { return d => d.Word; }
        }

        public object GetAttribute(DictionaryRecord record)
        {
            string result = pathResolver.ResolveToRelativePath(record.Word.PronuncationPath);
            return result;
        }
    }
}
