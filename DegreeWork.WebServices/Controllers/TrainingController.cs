using DegreeWork.Common.Entities;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DegreeWork.WebServices.Controllers
{
    public class TrainingController : ApiController
    {
        private readonly ITrainingRepository trainingRepository;

        public TrainingController(ITrainingRepository trainingRepository) 
        {
            this.trainingRepository = trainingRepository;
        }

        public Training Get(string trainingName)
        {
            Training training = trainingRepository.GetFirst(t => t.WidgetName == trainingName);
            return training;
        }
    }
}
