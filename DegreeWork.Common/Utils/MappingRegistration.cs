using DegreeWork.Common.Entities;
using DegreeWork.Common.ResourceManaging;
using DegreeWork.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common.Utils
{
    public class MappingRegistration
    {
        private Dictionary<string, Delegate> mappers;
        private IPathResolver pathResolver;

        public MappingRegistration(IPathResolver pathResolver)
        {
            mappers = new Dictionary<string, Delegate>();
            this.pathResolver = pathResolver;
            RegisterMappers();
        }

        public Delegate GetDelegate(Type first, Type second)
        {
            string key = GetKey(first, second);
            Delegate result = null;
            mappers.TryGetValue(key, out result);

            return result;
        }

        private string GetKey(Type first, Type second)
        {
            return first.MetadataToken.ToString() + second.MetadataToken.ToString();
        }

        private void RegisterMappers()
        {
            Register<Word, WordViewModel>(w => new WordViewModel() {
                Word = w.Representation,
                Translations = w.Translations.Select(t => t.Representation).ToArray(),
                WordImages = w.WordImages.Select(img => pathResolver.ResolveToRelativePath(img.ImagePath)).ToArray()
            });

            Register<DictionaryRecord, DictionaryRecordViewModel>(r => new DictionaryRecordViewModel() {
                Id = r.Id,
                Word = r.Word.Representation,
                WordImage = pathResolver.ResolveToRelativePath(r.WordImage.ImagePath),
                Translations = r.Translations.Select(t => t.Representation).ToArray()
            });
        }

        private void Register<TFirst, TSecond>(Expression<Func<TFirst, TSecond>> mapping) where TSecond : class
        {
            ParameterExpression input = mapping.Parameters.First();
            Type inputType = input.Type;
            Type outputType = mapping.ReturnType;

            string key = GetKey(inputType, outputType);
            mappers[key] = mapping.Compile();
        }
    }
}
