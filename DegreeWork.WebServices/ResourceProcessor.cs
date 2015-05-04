using DegreeWork.Common.ResourceManaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DegreeWork.WebServices
{
    internal class ResourceProcessor : IResourceAllocator, IPathResolver
    {
        private string resourcesFolder;
        private string resourcesPath;

        public ResourceProcessor()
        {
            this.resourcesFolder = ConfigurationKeys.ResourcesFolder;
            this.resourcesPath = ConfigurationKeys.ResourcesPath;
        }

        public async Task<string> SaveAsync(string key, StreamDescriptor streamDescriptor, string filePrefix = null)
        {
            string fileName = Path.ChangeExtension(Guid.NewGuid().ToString().Split('-')[0], streamDescriptor.Extension);

            if(!String.IsNullOrEmpty(filePrefix)) 
                fileName = String.Format("{0}_{1}", filePrefix, fileName);

            string path = Path.Combine(
                resourcesFolder,
                key);

            if(!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path = Path.Combine(path, fileName);
            using(FileStream fs = File.Create(path))
                await streamDescriptor.Stream.CopyToAsync(fs);

            string token = path.Substring(resourcesFolder.Length);
            return token;
        }

        public string ResolveToRelativePath(string token)
        {
            string result = token;
            if(!token.StartsWith(resourcesPath))
                result = resourcesPath + token;

            return result;
        }

        public string GetToken(string relativePath)
        {
            return relativePath.Substring(resourcesPath.Length);
        }
    }
}