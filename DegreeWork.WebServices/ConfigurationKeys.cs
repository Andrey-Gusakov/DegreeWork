using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DegreeWork.WebServices
{
    static class ConfigurationKeys
    {
        public static string SpaPhysicalFileSystemPath 
        {
            get
            {
                return ConfigurationManager.AppSettings["spaDirectory"];
            }
        }

        public static string ResourcesFolder
        {
            get
            {
                return ConfigurationManager.AppSettings["resourcesDirectory"];
            }
        }

        public static string ResourcesPath
        {
            get
            {
                return ConfigurationManager.AppSettings["resourcesPath"];
            }
        }
    }
}