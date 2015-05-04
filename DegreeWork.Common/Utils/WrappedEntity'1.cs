using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Utils
{
    /*internal class WrappedEntity<T> : IWrappedEntity<T> where T : class
    {
        private MetadataWrapper metadata;

        public T Entity { get; private set; }

        public IPagingData PagingData
        {
            get { return metadata; }
        }

        public ISortingData SortingData
        {
            get { return metadata; }
        }

        public WrappedEntity(T entity, object obj)
        {
            this.Entity = entity;
            metadata = new MetadataWrapper(obj);
        }

        private class MetadataWrapper : IPagingData, ISortingData
        {
            public MetadataWrapper(object obj)
            {
                IPagingData pagingData = obj as IPagingData;
                ISortingData sortingData = obj as ISortingData;
            }

            public int Page
            {
                get { return pagin; }
            }

            public int PageSize
            {
                get { throw new NotImplementedException(); }
            }

            public System.Linq.Expressions.Expression SortingExpression
            {
                get { throw new NotImplementedException(); }
            }

            public Enums.SortOrder SortOrder
            {
                get { throw new NotImplementedException(); }
            }
        }
    }*/
}
