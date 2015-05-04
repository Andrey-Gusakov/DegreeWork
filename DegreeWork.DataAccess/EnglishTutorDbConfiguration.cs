using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.DataAccess
{
    class EnglishTutorDbConfiguration : DbConfiguration
    {
        public EnglishTutorDbConfiguration()
        {
            SetDatabaseInitializer<EnglishTutorDbContext>(new CreateInitializer());
        }
    }
}
