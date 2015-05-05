using Autofac.Features.Indexed;
using DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting;
using DegreeWork.Common;
using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers
{
    class TrainingWordComposer
    {
        private readonly IEnumerable<IRecordAttributeAware> informators;
        private readonly Func<Expression, IExpressionResolver> resolverBuilder;

        public TrainingWordComposer(IEnumerable<IRecordAttributeAware> informators, 
            Func<Expression, IExpressionResolver> resolverBuilder)
        {
            this.informators = informators;
            this.resolverBuilder = resolverBuilder;
        }

        public Dictionary<string, object>[] ComposeWordsArray(WordAttributes[] attributes,
            Func<Expression<Func<RecordStatistic, object>>[], List<RecordStatistic>> wordsGetter)
        {
            IRecordAttributeAware[] activeInformators = attributes.Select(attr => informators.First(worker => worker.Attribute == attr)).ToArray();
            Expression<Func<RecordStatistic, object>>[] includes = activeInformators.Select(i => RewriteExpression(i.IncludeExpression)).ToArray();
            List<RecordStatistic> records = wordsGetter(includes);

            Dictionary<string, object>[] result = Enumerable
                .Repeat<Func<Dictionary<string, object>>>(() => new Dictionary<string, object>(), records.Count)
                .Select(f => f())
                .ToArray();

            foreach(IRecordAttributeAware informator in activeInformators)
            {
                string key = informator.Attribute.ToString().ToLower();
                IRecordAttributeTamper tamper = informator as IRecordAttributeTamper;
                Func<DictionaryRecord, object> tamp = tamper != null ? tamper.GetAttribute : informator.IncludeExpression.Compile();
                for(int i = 0; i < records.Count; i++)
                    result[i][key] = tamp(records[i].DictionaryRecord);
            }

            return result;
        }

        private Expression<Func<RecordStatistic, object>> RewriteExpression(Expression<Func<DictionaryRecord, object>> expression)
        {
            Expression nextInChain = expression.Body;

            Stack<IExpressionResolver> resolvers = new Stack<IExpressionResolver>();
            IExpressionResolver currentResolver = resolverBuilder(nextInChain);
            resolvers.Push(currentResolver);
            while(!currentResolver.IsReachType(typeof(DictionaryRecord)))
            {
                nextInChain = currentResolver.UnderlyingExpression;
                currentResolver = resolverBuilder(nextInChain);
                resolvers.Push(currentResolver);
            }

            ParameterExpression aParam = ParameterExpression.Parameter(typeof(RecordStatistic));
            PropertyInfo propertyInfo = typeof(RecordStatistic).GetProperty("DictionaryRecord");
            Expression newExpression = MemberExpression.Property(aParam, propertyInfo);
            while(resolvers.Count > 0)
                newExpression = resolvers.Pop().UpdateExpression(newExpression);

            Expression<Func<RecordStatistic, object>> newLambdaExpr = Expression.Lambda<Func<RecordStatistic, object>>(newExpression, aParam);
            return newLambdaExpr;
        }
    }
}
