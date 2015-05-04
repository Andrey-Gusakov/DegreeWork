using Autofac;
using DegreeWork.BusinessLogic.ExternalApi.ApiInvokers;
using DegreeWork.BusinessLogic.ExternalApi.ApiResultFactories;
using DegreeWork.BusinessLogic.Services;
using DegreeWork.BusinessLogic.Services.InternalInterfaces;
using DegreeWork.Common;
using DegreeWork.Common.ExternalApiUtils.Abstractions;
using DegreeWork.Common.Interfaces.Services;
using DegreeWork.Common.ResourceProcessing.Abstractions;
using DegreeWork.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.BusinessLogic
{
    public class BusinessLogicAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<WordService>().As<IWordService>().As<IInternalWordService>();
            builder.RegisterType<DictionaryService>().As<IDictionaryService>();

            builder.RegisterType<MainTokensApiInvoker>().As<ApiInvoker>();
            builder.RegisterType<ImageProviderApiInvoker>().As<ApiInvoker>();
            builder.RegisterType<SpeechProviderDataMarketApiInvoker>().As<ApiInvoker>();

            builder.RegisterType<SpeechApiResultFactory>();
            builder.RegisterType<MainTokensApiResultFactory>();
            builder.RegisterType<ImageApiResultFactory>();

            builder.RegisterModule<DataAccessAutofacModule>();
            builder.RegisterModule<CommonAutofacModule>();
        }
    }
}
