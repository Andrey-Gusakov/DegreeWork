using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.ResourceManaging
{
    public interface IPathResolver
    {
        string ResolveToRelativePath(string token);
        string GetToken(string relativePath);
    }
}
