using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DegreeWork.Common.Interfaces.DatabaseInterfaces
{
    public interface IDatabaseContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken token);

        Type GetUnderlyingType(Type type);
    }
}
