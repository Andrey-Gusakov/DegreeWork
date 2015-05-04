using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Entities
{
    public class RecordStatistic : IAuditEntity
    {
        public int Id { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswers { get; set; }
        public DateTime LastAccessTime { get; set; }
        public DateTime NextTrainingTime { get; set; }

        public virtual DictionaryRecord DictionaryRecord { get; set; }
        public virtual Training Training { get; set; }
    }
}
