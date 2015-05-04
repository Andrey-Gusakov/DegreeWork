using DegreeWork.Common.Entities;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.DataAccess.Repositories
{
    class TrainingRepository : BaseRepository<Training>, ITrainingRepository
    {
        public TrainingRepository(DbContext context) : base(context) { }
    }
}