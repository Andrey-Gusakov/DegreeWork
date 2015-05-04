using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Utils.Interfaces
{
    public interface IWrappedEntity<T> where T : class
    {
        T Entity { get; }
        IPagingData PagingData { get; }
        ISortingData SortingData { get; }
    }
}
