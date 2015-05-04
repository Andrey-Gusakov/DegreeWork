using DegreeWork.BusinessLogic.Helpers;
using DegreeWork.BusinessLogic.Services.InternalInterfaces;
using DegreeWork.Common.Entities;
using DegreeWork.Common.ExternalApiUtils;
using DegreeWork.Common.ExternalApiUtils.Models;
using DegreeWork.Common.Interfaces;
using DegreeWork.Common.Interfaces.DatabaseInterfaces;
using DegreeWork.Common.Interfaces.Services;
using DegreeWork.Common.ResourceManaging;
using DegreeWork.Common.Utils.Interfaces;
using DegreeWork.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic.Services
{
    class WordService : IWordService, IInternalWordService
    {
        private readonly IWordRepository wordRepository;
        private readonly IModelMapper modelMapper;
        private readonly IPathResolver pathResolver;
        private readonly Func<ExternalApisManager> apisManagerFactory;
        private readonly IDatabaseContext databaseContext;

        public WordService(IWordRepository wordRepository, 
            IModelMapper modelMapper, 
            IPathResolver pathResolver,
            IDatabaseContext databaseContext,
            Func<ExternalApisManager> apisManagerFactory)
        {
            this.wordRepository = wordRepository;
            this.modelMapper = modelMapper;
            this.pathResolver = pathResolver;
            this.databaseContext = databaseContext;
            this.apisManagerFactory = apisManagerFactory;
        }

        public async Task<WordViewModel> GetWordData(string word)
        {
            Word wordModel = GetWordLocally(word);

            if(wordModel == null)
            {
                ExternalApisManager apisManager = apisManagerFactory();
                ApisInvokationResult invokationResult = await apisManager.CollectResources(word);
                if(invokationResult != null)
                {
                    wordModel = new Word() { Representation = word };
                    invokationResult.UpdateModel(wordModel);
                    wordRepository.Add(wordModel);
                    await databaseContext.SaveChangesAsync();
                }
            }

            WordViewModel result = modelMapper.Map<WordViewModel>(wordModel);
            result.WordImages = result.WordImages.Select(img => pathResolver.ResolveToRelativePath(img)).ToArray();
            return result;
        }

        public Word GetWordLocally(string word)
        {
            DbRequestMetainfoBuilder builder = new DbRequestMetainfoBuilder();
            IDbRequestMetainfo metaInfo = builder.AddPaging(0, 1).GetRequestMetainfo();
            Word wordModel = wordRepository.Get(w => w.Representation == word, metaInfo,
                w => w.Translations,
                w => w.WordImages
            ).FirstOrDefault();

            return wordModel;
        }
    }
}
