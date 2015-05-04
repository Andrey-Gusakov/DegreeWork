using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Entities
{
    public class DictionaryRecord : IAuditEntity, ISecureEntity
    {
        public int Id { get; set; }

        public User User { get; set; }

        public int UserId { get; set; }

        public Word Word { get; set; }

        public Thesaurus Thesaurus { get; set; }

        public WordImage WordImage { get; set; }

        [InverseProperty("DictionaryRecord")]
        public virtual ICollection<RecordStatistic> Statistics { get; set; }

        public virtual ICollection<Translation> Translations { get; set; }
    }
}
