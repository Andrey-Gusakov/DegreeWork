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

        public DbRequestMetainfoBuilder AddPaging(int skip, int take) 
        {
            context.TakeInternal = take;
            context.SkipInternal = skip;

            return this;
        }

        public DbRequestMetainfoBuilder AddPaging(IPagingData pagingData)
        {
            context.TakeInternal = pagingData.Take;
            context.SkipInternal = pagingData.Skip;

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
            DbRequestMetainfo result = new DbRequestMetainfo(context.TakeInternal != null ? context : null,
                context.SortingExpression != null ? context : null);

            context = new MetainfoContext();

            return result;
        }


        private class MetainfoContext : IPagingData, ISortingData 
        {
            public int? TakeInternal;
            public int? SkipInternal;
            public SortOrder? SortOrderInternal;

            public int Take {
                get { return TakeInternal.Value; }
            }

            public int Skip {
                get { return SkipInternal.Value; } 
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
