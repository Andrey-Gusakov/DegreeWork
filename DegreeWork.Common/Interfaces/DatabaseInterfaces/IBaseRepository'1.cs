using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Interfaces.DatabaseInterfaces
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);
        T Add(T entity);
        T Update(T entity);
        T GetById(object id);
        T GetFirst(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        void Remove(object id);
        void Remove(T entityToRemove);
        List<T> Get(Expression<Func<T, bool>> filter, IDbRequestMetainfo metainfo, params Expression<Func<T, object>>[] includes);
        List<T> Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    }
}
