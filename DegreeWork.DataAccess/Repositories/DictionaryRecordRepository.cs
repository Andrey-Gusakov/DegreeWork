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
    internal class DictionaryRecordRepository : BaseRepository<DictionaryRecord>, IDictionaryRecordRepository
    {
        public DictionaryRecordRepository(DbContext context) : base(context) { }
    }
}
