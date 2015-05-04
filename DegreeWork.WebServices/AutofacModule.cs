using Autofac;
using DegreeWork.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac.Integration.WebApi;
using DegreeWork.Common.ResourceManaging;

namespace DegreeWork.WebServices
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(this.GetType().Assembly).PropertiesAutowired();

            builder.RegisterType<ResourceProcessor>()
                .As<IResourceAllocator>()
                .As<IPathResolver>()
                .InstancePerLifetimeScope();

            builder.RegisterModule<BusinessLogicAutofacModule>();
        }
    }
}