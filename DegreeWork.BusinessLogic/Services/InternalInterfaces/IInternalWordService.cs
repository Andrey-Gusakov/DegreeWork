using DegreeWork.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Services.InternalInterfaces
{
    internal interface IInternalWordService
    {
        Word GetWordLocally(string word);
    }
}
