using DegreeWork.Common.Entities;
using DegreeWork.Common.Enums;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.ResourceManaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Helpers.WordBuilders
{
    class ImageAware : IRecordAttributeTamper
    {
        private IPathResolver pathResolver;

        public ImageAware(IPathResolver pathResolver)
        {
            this.pathResolver = pathResolver;
        }

        public WordAttributes Attribute
        {
            get { return WordAttributes.Image; }
        }

        public Expression<Func<DictionaryRecord, object>> IncludeExpression
        {
            get { return d => d.WordImage; }
        }

        public object GetAttribute(DictionaryRecord record)
        {
            string result = pathResolver.ResolveToRelativePath(record.WordImage.ImagePath);
            return result;
        }
    }
}
