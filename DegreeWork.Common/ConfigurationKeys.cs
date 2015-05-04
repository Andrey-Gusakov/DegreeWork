using DegreeWork.Common.ExternalApiUtils.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common
{
    internal static class ConfigurationKeys
    {
        public static ApiLoginModel MicrosoftOAuthLoginModel
        {
            get 
            {
                return new ApiLoginModel() {
                    ClientId = ConfigurationManager.AppSettings["microsoftOathClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["microsoftOathClientSecret"]
                };
            }
        }
    }
}
