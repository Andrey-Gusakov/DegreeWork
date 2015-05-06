using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Interfaces
{
    public interface IRecordAttributeTamper
    {
        WordAttributes Attribute { get; }
        Expression<Func<DictionaryRecord, object>> IncludeExpression { get; }
        object GetAttribute(DictionaryRecord record);
    }
}
