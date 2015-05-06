using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ViewModels
{
    public class TrainingWordsRequestViewModel : IPagingData
    {
        public int TrainingId { get; set; }
        public WordAttributes[] WordAttributes { get; set; }
        public int Count { get; set; }

        public int Page
        {
            get { return 0; }
        }

        public int PageSize
        {
            get { return Count; }
        }
    }
}
