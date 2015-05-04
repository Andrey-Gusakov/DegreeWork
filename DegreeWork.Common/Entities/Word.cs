using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Entities
{
    public class Word : IAuditEntity
    {
        public int Id { get; set; }
        public string Representation { get; set; }
        public string PronuncationPath { get; set; }

        public virtual ICollection<WordImage> WordImages { get; set; }
        public virtual ICollection<Translation> Translations { get; set; }
        public virtual ICollection<Thesaurus> Thesauruses { get; set; }
    }
}
