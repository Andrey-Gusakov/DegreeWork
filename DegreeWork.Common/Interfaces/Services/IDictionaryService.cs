using DegreeWork.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Interfaces.Services
{
    public interface IDictionaryService
    {
        DictionaryRecordViewModel AddRecord(DictionaryRecordViewModel record);
        bool UpdateRecord(DictionaryRecordViewModel record);
        IEnumerable<DictionaryRecordViewModel> GetRecords(IUserContext user);
    }
}
