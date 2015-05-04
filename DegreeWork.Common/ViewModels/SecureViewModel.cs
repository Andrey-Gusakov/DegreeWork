using DegreeWork.Common.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ViewModels
{
    public class SecureViewModel : ISecurityContextHolder
    {
        [JsonIgnore]
        public IUserContext UserContext { get; set; }
    }
}
