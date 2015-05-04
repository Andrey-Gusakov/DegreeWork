using DegreeWork.BusinessLogic.Helpers;
using DegreeWork.Common.Entities;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using DegreeWork.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Services
{
    class TrainingService : ITrainingService
    {
        private readonly ITrainingRepository repository;

        public TrainingService(ITrainingRepository repository)
        {
            this.repository = repository;
        }

        public Training GetTraining(string name)
        {
        }
    }
}
