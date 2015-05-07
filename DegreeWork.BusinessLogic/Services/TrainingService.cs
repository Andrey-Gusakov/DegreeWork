using DegreeWork.BusinessLogic.Helpers;
using DegreeWork.BusinessLogic.Helpers.IncludeExpressionRewriting;
using DegreeWork.Common;
using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using DegreeWork.Common.Interfaces.Services;
using DegreeWork.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Services
{
    class TrainingService : ITrainingService
    {
        private readonly ITrainingWordsRepository trainingWordsRepository;
        private readonly Func<TrainingWordComposer> composerBuilder;
        private readonly IDictionaryRecordRepository dictionaryRepository;
        private readonly Func<Expression, IExpressionResolver> resolverBuilder;

        public TrainingService(ITrainingWordsRepository trainingWordsRepository,
            IDictionaryRecordRepository dictionaryRepository,
            Func<TrainingWordComposer> composerBuilder,
            Func<Expression, IExpressionResolver> resolverBuilder)
        {
            this.trainingWordsRepository = trainingWordsRepository;
            this.dictionaryRepository = dictionaryRepository;
            this.composerBuilder = composerBuilder;
            this.resolverBuilder = resolverBuilder;
        }

        public Dictionary<string, object>[] GetWords(TrainingWordsRequestViewModel trainingViewModel, IUserContext context)
        {
            DbRequestMetainfoBuilder builder = new DbRequestMetainfoBuilder();
            builder.AddPaging(trainingViewModel);

            TrainingWordComposer composer = composerBuilder();
            Dictionary<string, object>[] result = composer.ComposeWordsArray(trainingViewModel.WordAttributes, 
                includes => {
                    int trainingId = trainingViewModel.TrainingId;
                    List<DictionaryRecord> res = GetNewRecords(trainingId, context, builder, includes);
                    if(res.Count < trainingViewModel.Take)
                    {
                        builder.AddPaging(0, trainingViewModel.Take - res.Count);
                        List<DictionaryRecord> oldRecords = GetRecords(trainingId, context, builder, includes);
                        res.AddRange(oldRecords);
                    }
                    return res;
                });

            return result;
        }

        public List<DictionaryRecord> GetNewRecords(int trainigId, IUserContext context, 
            DbRequestMetainfoBuilder builder,
            Expression<Func<DictionaryRecord, object>>[] includes)
        {
            return dictionaryRepository.Get(r => !r.Statistics.Any(s => s.Training.Id == trainigId),
                builder.GetRequestMetainfo(),
                includes);
        }

        private List<DictionaryRecord> GetRecords(int trainingId, IUserContext context, 
            DbRequestMetainfoBuilder builder,
            Expression<Func<DictionaryRecord, object>>[] includes)
        {
            Expression<Func<RecordStatistic, bool>> filter =
                r => r.Training.Id == trainingId && r.DictionaryRecord.UserId == context.Id &&
                    (r.NextTrainingTime < DateTime.Now || r.NextTrainingTime == Constants.DummyDate);

            Expression<Func<RecordStatistic, object>>[] rewroteIncludes = includes.Select(RewriteExpression).ToArray();
            builder.AddSorting<RecordStatistic>(r => r.NextTrainingTime.ToString());
            List<RecordStatistic> dbResult = trainingWordsRepository.Get(filter, builder.GetRequestMetainfo(), rewroteIncludes);
            List<DictionaryRecord> result = dbResult.Select(r => r.DictionaryRecord).ToList();
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
