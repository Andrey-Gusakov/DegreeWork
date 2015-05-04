using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ViewModels
{
    public class DictionaryRecordViewModel : SecureViewModel
    {
        public int? Id { get; set; }
        public string Word { get; set; }
        public string WordImage { get; set; }
        public string[] Translations { get; set; }

        public WordViewModel WordModel { get; set; }
    }
}
