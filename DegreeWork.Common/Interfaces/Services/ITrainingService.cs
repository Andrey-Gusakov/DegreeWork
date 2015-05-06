using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Interfaces.Services
{
    public interface ITrainingService
    {
        Dictionary<string, object>[] GetWords(TrainingWordsRequestViewModel trainingViewModel, IUserContext userContext);
    }
}
