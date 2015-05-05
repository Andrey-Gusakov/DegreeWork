using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Interfaces.Services
{
    public interface ITrainingService
    {
        Dictionary<string, object>[] GetWords(int trainingId, IUserContext userContext, IPagingData pagingData, WordAttributes[] attributes);
    }
}
