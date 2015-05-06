using DegreeWork.Common.Entities;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using DegreeWork.Common.Interfaces.Services;
using DegreeWork.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DegreeWork.WebServices.Controllers
{
    public class TrainingController : BaseController
    {
        private readonly ITrainingRepository trainingRepository;
        private readonly ITrainingService trainingService;

        public TrainingController(ITrainingRepository trainingRepository, ITrainingService trainingService)
        {
            this.trainingService = trainingService;
            this.trainingRepository = trainingRepository;
        }

        public Training Get(string trainingName)
        {
            Training training = trainingRepository.GetFirst(t => t.Name == trainingName);
            return training;
        }

        [Route("api/training/words")]
        [HttpPost]
        public Dictionary<string, object>[] GetWords([FromBody]TrainingWordsRequestViewModel trainingViewModel)
        {
            return trainingService.GetWords(trainingViewModel, UserContext);
        }
    }
}
