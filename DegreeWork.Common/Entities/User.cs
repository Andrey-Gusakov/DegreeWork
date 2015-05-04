using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Entities
{
    public class User : IAuditEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Training> Trainings { get; set; }
        public virtual ICollection<DictionaryRecord> DictionaryRecords { get; set; }

    }
}
