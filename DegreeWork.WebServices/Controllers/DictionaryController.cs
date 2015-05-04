using DegreeWork.Common.Interfaces.Services;
using DegreeWork.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DegreeWork.WebServices.Controllers
{
    public class DictionaryController : BaseController
    {
        public IDictionaryService dictionaryService;

        public DictionaryController(IDictionaryService dictionaryService)
        {
            this.dictionaryService = dictionaryService;
        }

        // GET api/<controller>
        public IEnumerable<DictionaryRecordViewModel> Get()
        {
            return dictionaryService.GetRecords(UserContext);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public DictionaryRecordViewModel Post([FromBody]DictionaryRecordViewModel recordViewModel)
        {
            recordViewModel.UserContext = UserContext;
            DictionaryRecordViewModel result = dictionaryService.AddRecord(recordViewModel);

            return result;
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]DictionaryRecordViewModel recordViewModel)
        {
            recordViewModel.UserContext = UserContext;
            dictionaryService.UpdateRecord(recordViewModel);
            CommitApplicator.Commit();
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}