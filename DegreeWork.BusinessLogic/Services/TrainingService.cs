using DegreeWork.BusinessLogic.Helpers;
using DegreeWork.Common;
using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using DegreeWork.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Services
{
    class TrainingService : ITrainingService
    {
        private readonly ITrainingWordsRepository trainingWordsRepository;
        private readonly Func<TrainingWordComposer> composerBuilder;

        public TrainingService(ITrainingWordsRepository trainingWordsRepository, Func<TrainingWordComposer> composerBuilder)
        {
            this.trainingWordsRepository = trainingWordsRepository;
            this.composerBuilder = composerBuilder;
        }

        public Dictionary<string, object>[] GetWords(int trainingId, IUserContext context, IPagingData pagingData, WordAttributes[] attributes)
        {
            Expression<Func<RecordStatistic, bool>> filter = 
                r => r.Training.Id == trainingId && r.DictionaryRecord.UserId == context.Id &&
                    (r.NextTrainingTime < DateTime.Now || r.NextTrainingTime == Constants.DummyDate);
            DbRequestMetainfoBuilder builder = new DbRequestMetainfoBuilder();
            builder.AddPaging(pagingData.Page, pagingData.PageSize);
            builder.AddSorting<RecordStatistic>(r => r.NextTrainingTime);

            TrainingWordComposer composer = composerBuilder();
            Dictionary<string, object>[] result = composer.ComposeWordsArray(attributes, 
                includes => trainingWordsRepository.Get(filter, builder.GetRequestMetainfo(), includes));

            return result;
        }
    }
}
