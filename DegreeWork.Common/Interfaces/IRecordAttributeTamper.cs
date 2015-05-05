using DegreeWork.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Interfaces
{
    public interface IRecordAttributeTamper : IRecordAttributeAware
    {
        object GetAttribute(DictionaryRecord record);
    }
}
