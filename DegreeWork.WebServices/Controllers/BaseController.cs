using DegreeWork.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DegreeWork.WebServices.Controllers
{
    public class BaseController : ApiController
    {
        public IUserContext UserContext
        {
            get { return new UserContextImplementation() { Id = 1, Name = "Andrey" }; }
        }

        public ICommitApplicator CommitApplicator { get; set; }

        private class UserContextImplementation : IUserContext
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }
    }
}
