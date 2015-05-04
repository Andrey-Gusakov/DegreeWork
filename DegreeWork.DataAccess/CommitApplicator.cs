using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.DataAccess
{
    internal class CommitApplicator : ICommitApplicator
    {
        private Func<DbContext> dbContextFactory;
        private DbContext dbContext;

        public CommitApplicator(Func<DbContext> dbContextFactory) 
        {
            this.dbContextFactory = dbContextFactory;
        }

        public void Commit()
        {
            if(dbContext == null)
                dbContext = dbContextFactory();

            dbContext.SaveChanges();
        }
    }
}
