using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers
{
    public class DbRequestMetainfoBuilder
    {
        MetainfoContext context;

        public DbRequestMetainfoBuilder() 
        {
            context = new MetainfoContext();
        }

        public DbRequestMetainfoBuilder AddPaging(int page, int pageSize) 
        {
            context.PageInternal = page;
            context.PageSizeInternal = pageSize;

            return this;
        }

        public DbRequestMetainfoBuilder AddPaging(IPagingData pagingData)
        {
            context.PageInternal = pagingData.Page;
            context.PageSizeInternal = pagingData.PageSize;

            return this;
        }

        public DbRequestMetainfoBuilder AddSorting<T>(Expression<Func<T, object>> sortingExpression, 
            SortOrder sortOrder = SortOrder.Ascending) where T : class
        {
            context.SortingExpression = sortingExpression;
            context.SortOrderInternal = sortOrder;
            return this;
        }

        public IDbRequestMetainfo GetRequestMetainfo() 
        {
            DbRequestMetainfo result = new DbRequestMetainfo(context.PageInternal != null ? context : null,
                context.SortingExpression != null ? context : null);

            context = new MetainfoContext();

            return result;
        }


        private class MetainfoContext : IPagingData, ISortingData 
        {
            public int? PageInternal;
            public int? PageSizeInternal;
            public SortOrder? SortOrderInternal;

            public int Page {
                get { return PageInternal.Value; }
            }

            public int PageSize {
                get { return PageSizeInternal.Value; } 
            }

            public Expression SortingExpression { get; set; }

            public SortOrder SortOrder
            {
                get { return SortOrderInternal.Value; }
            }
        }

        private class DbRequestMetainfo : IDbRequestMetainfo
        {
            private IPagingData pagingData;
            private ISortingData sortingData;

            public DbRequestMetainfo(IPagingData pagingData, ISortingData sortingData)
            {
                this.pagingData = pagingData;
                this.sortingData = sortingData;
            }
        
            public IPagingData PagingData
            {
                get { return pagingData; }
            }

            public ISortingData SortingData
            {
	              get { return sortingData; }
            }
        }
    }
}
