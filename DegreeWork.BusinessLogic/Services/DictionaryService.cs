using DegreeWork.BusinessLogic.Services.InternalInterfaces;
using DegreeWork.Common.Entities;
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
    class DictionaryService : IDictionaryService
    {
        private readonly IDictionaryRecordRepository dictionaryRepository;
        private readonly IInternalWordService wordService;
        private readonly IModelMapper modelMapper;
        private readonly IPathResolver pathResolver;
        private readonly IDatabaseContext dbContext;

        public DictionaryService(IDictionaryRecordRepository dictionaryRepository, IInternalWordService wordService,
            IDatabaseContext dbContext, IPathResolver pathResolver, IModelMapper modelMapper)
        {
            this.dictionaryRepository = dictionaryRepository;
            this.wordService = wordService;
            this.pathResolver = pathResolver;
            this.modelMapper = modelMapper;
            this.dbContext = dbContext;
        }

        public DictionaryRecordViewModel AddRecord(DictionaryRecordViewModel recordViewModel)
        {
            DictionaryRecord record = BuildRecord(recordViewModel);
            record.Translations = record.Word
                .Translations
                .Where(t => recordViewModel.Translations.Contains(t.Representation))
                .ToArray();

            record = dictionaryRepository.Add(record);
            dbContext.SaveChanges();

            recordViewModel.Id = record.Id;
            recordViewModel.WordModel = modelMapper.Map<WordViewModel>(record.Word);

            return recordViewModel;
        }

        public bool UpdateRecord(DictionaryRecordViewModel recordViewModel)
        {
            DictionaryRecord record = BuildRecord(recordViewModel);
            List<Translation> translationsToRemove = record.Translations
                .Where(t => recordViewModel.Translations.All(rvmT => rvmT != t.Representation)).ToList();
            foreach(Translation translation in translationsToRemove)
                record.Translations.Remove(translation);

            foreach(Translation translationToAdd in recordViewModel.Translations
                .Where(rvmT => !record.Translations.Any(t => t.Representation == rvmT))
                .Select(rvmT => record.Word.Translations.First(t => t.Representation == rvmT)))
            {
                record.Translations.Add(translationToAdd);
            }

            return true;
        }
        

        public IEnumerable<DictionaryRecordViewModel> GetRecords(IUserContext user)
        {
            List<DictionaryRecord> records = dictionaryRepository.Get(r => r.UserId == user.Id, r => r.Word, r => r.WordImage);
            IEnumerable<DictionaryRecordViewModel> result = records.Select(r => {
                DictionaryRecordViewModel viewModel = modelMapper.Map<DictionaryRecordViewModel>(r);
                viewModel.WordModel = modelMapper.Map<WordViewModel>(r.Word);
                return viewModel;
            });

            return result;
        }

        private DictionaryRecord BuildRecord(DictionaryRecordViewModel recordViewModel)
        {
            Word word = wordService.GetWordLocally(recordViewModel.Word);
            DictionaryRecord record = recordViewModel.Id.HasValue ? 
                dictionaryRepository.GetById(recordViewModel.Id.Value) : new DictionaryRecord();

            record.Word = word;
            record.WordImage = word.WordImages.First(img => img.ImagePath == pathResolver.GetToken(recordViewModel.WordImage));
            record.UserId = recordViewModel.UserContext.Id;

            return record;
        }
    }
}
