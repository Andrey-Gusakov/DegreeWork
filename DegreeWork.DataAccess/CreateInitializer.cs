using DegreeWork.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.DataAccess
{
    class CreateInitializer : CreateDatabaseIfNotExists<EnglishTutorDbContext>
    {
        protected override void Seed(EnglishTutorDbContext context)
        {
            context.Users.Add(new User() {
                Name = "Andrey"
            });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
