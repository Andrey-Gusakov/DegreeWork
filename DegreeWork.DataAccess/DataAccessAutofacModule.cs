using Autofac;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using DegreeWork.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.DataAccess
{
    public class DataAccessAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<EnglishTutorDbContext>()
                .As<IDatabaseContext>()
                .As<DbContext>()
                .InstancePerRequest();

            builder.RegisterType<WordRepository>().As<IWordRepository>().InstancePerRequest();
            builder.RegisterType<DictionaryRecordRepository>().As<IDictionaryRecordRepository>().InstancePerRequest();
            
            builder.RegisterType<CommitApplicator>().As<ICommitApplicator>().InstancePerRequest();
        }
    }
}
