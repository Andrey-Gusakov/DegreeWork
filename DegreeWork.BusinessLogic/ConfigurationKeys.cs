using DegreeWork.Common.ExternalApiUtils.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic
{
    internal static class ConfigurationKeys
    {
        public static ApiLoginModel MicrosoftOAuthLoginModel
        {
            get 
            {
                return new ApiLoginModel() {
                    ClientId = ConfigurationManager.AppSettings["microsoftOauthClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["microsoftOauthClientSecret"]
                };
            }
        }

        public static ApiLoginModel MicrosoftAccountKeyLoginModel
        {
            get
            {
                return new ApiLoginModel() {
                    ClientId = ConfigurationManager.AppSettings["microsoftAccountClientId"],
                    ClientSecret = ConfigurationManager.AppSettings["microsoftAccountClientSecret"]
                };
            }
        }
    }
}
