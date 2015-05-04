using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Autofac;
using Autofac.Integration.WebApi;
using Pysco68.Owin.Logging.NLogAdapter;
using Newtonsoft.Json.Serialization;

[assembly: OwinStartup(typeof(DegreeWork.WebServices.Startup))]

namespace DegreeWork.WebServices
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterModule<AutofacModule>();
            IContainer container = containerBuilder.Build();

            app.UseAutofacMiddleware(container);

            ConfigureAuth(app);

            HttpConfiguration config = new HttpConfiguration();
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "WebServices",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            config.Formatters.JsonFormatter.SerializerSettings = new Newtonsoft.Json.JsonSerializerSettings() {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            app.UseNLog();
            app.UseAutofacMiddleware(container);
            app.UseWebApi(config);

            app.UseFileServer(new FileServerOptions() {
                RequestPath = PathString.Empty,
                FileSystem = new PhysicalFileSystem(ConfigurationKeys.SpaPhysicalFileSystemPath)
            });
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            //config.SuppressDefaultHostAuthentication();
        }
    }
}
