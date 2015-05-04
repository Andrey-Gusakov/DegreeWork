using Autofac;
using DegreeWork.Common.ExternalApiUtils;
using DegreeWork.Common.ResourceManaging;
using DegreeWork.Common.Utils;
using DegreeWork.Common.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreeWork.Common
{
    public class CommonAutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ResourceManager>();
            builder.RegisterType<ExternalApisManager>();
            builder.RegisterType<ModelMapper>().As<IModelMapper>();
            builder.RegisterType<MappingRegistration>().SingleInstance();
        }
    }
}
