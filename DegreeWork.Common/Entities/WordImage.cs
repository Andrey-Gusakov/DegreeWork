using DegreeWork.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Entities
{
    public class WordImage : IAuditEntity
    {
        public int Id { get; set; }
        public string ImagePath { get; set; }
    }
}
