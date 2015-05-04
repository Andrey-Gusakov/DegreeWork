using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using System.Data.Entity.Core.Objects;
using DegreeWork.DataAccess;

namespace DegreeWork
{
    public class EnglishTutorDbContext : DbContext, IDatabaseContext
    {
        public EnglishTutorDbContext() : base("EnglishTutorDbContext")
        {
            Database.SetInitializer<EnglishTutorDbContext>(new CreateInitializer());
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Word> Words { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<WordImage> WordImages { get; set; }
        public DbSet<Training> Trainings { get; set; }
        public DbSet<DictionaryRecord> DictionaryRecords { get; set; }
        public DbSet<RecordStatistic> RecordStatistics { get; set; }

        public Type GetUnderlyingType(Type type)
        {
            return ObjectContext.GetObjectType(type);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasMany(u => u.Trainings).WithMany();
            modelBuilder.Entity<DictionaryRecord>().HasMany(d => d.Translations).WithMany();
        }
    }
}
