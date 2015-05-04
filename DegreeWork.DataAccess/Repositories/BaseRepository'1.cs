using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.DataAccess.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class, IAuditEntity
    {
        private DbSet<T> innerSet;
        private DbContext context;

        public BaseRepository(DbContext context)
        {
            this.innerSet = context.Set<T>();
            this.context = context;
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            return ApplyIncludes(innerSet, includes);
        }

        public T Add(T entity)
        {
            T result = innerSet.Add(entity);
            return result;
        }

        public T Update(T entity)
        {
            entity = innerSet.Attach(entity);
            DbEntityEntry<T> entry = context.Entry<T>(entity);
            entry.State = EntityState.Modified;
            if(entry.State == EntityState.Detached)
            {
                
                //entry.State = EntityState.Modified;
            }

            return entity;
        }

        public T GetById(object id)
        {
            return innerSet.Find(id);
        }

        public void Remove(object id) 
        {
            T entityToRemove = GetById(id);
            Remove(entityToRemove);
        }

        public void Remove(T entityToRemove)
        {
            innerSet.Remove(entityToRemove);
        }

        public T GetFirst(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = GetInternal(filter, null, includes);
            return query.FirstOrDefault();
        }

        public List<T> Get(Expression<Func<T, bool>> filter, 
            IDbRequestMetainfo metainfo, 
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = GetInternal(filter, metainfo, includes);

            List<T> result = query.ToList();
            return result;
        }

        private IQueryable<T> GetInternal(Expression<Func<T, bool>> filter, 
            IDbRequestMetainfo metainfo, 
            params Expression<Func<T, object>>[] includes) 
        {
            IQueryable<T> query = innerSet.Where(filter);

            if(metainfo != null)
            {
                if(metainfo.SortingData != null)
                {
                    ISortingData sortingData = metainfo.SortingData;
                    var sortingExpression = sortingData.SortingExpression as Expression<Func<T, object>>;
                    if(sortingData.SortOrder == SortOrder.Ascending)
                        query = query.OrderBy(sortingExpression);
                    else
                        query = query.OrderByDescending(sortingExpression);
                }

                if(metainfo.PagingData != null)
                {
                    IPagingData pagingData = metainfo.PagingData;
                    if(metainfo.SortingData == null)
                        query = query.OrderBy(e => e.Id);

                    query = query.Skip(pagingData.Page * pagingData.PageSize).Take(pagingData.PageSize);
                }
            }

            query = ApplyIncludes(query, includes);
            return query;
        }

        public List<T> Get(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            return Get(filter, null, includes);
        }


        private IQueryable<T> ApplyIncludes(IQueryable<T> query, params Expression<Func<T, object>>[] includes)
        {
            if(includes == null || includes.Length == 0)
                return query;

            for(int i = 0; i < includes.Length; i++)
                query = query.Include(includes[i]);

            return query;
        }
    }
}
