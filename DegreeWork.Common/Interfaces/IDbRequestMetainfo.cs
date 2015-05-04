using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Interfaces
{
    public interface IDbRequestMetainfo
    {
        IPagingData PagingData { get; }
        ISortingData SortingData { get; }
    }
}
