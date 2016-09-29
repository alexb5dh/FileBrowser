using System.Net.Http.Formatting;
using System.Web.Http;
using FileBrowser.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

namespace FileBrowser
{
    // Todo: remove angular folder
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            ConfigureMediaFormatters(config);
            RegisterDependencies(config);
            MapRoutes(config);
        }

        private static void ConfigureMediaFormatters(HttpConfiguration config)
        {
            config.Formatters.Clear();

            config.Formatters.Add(new JsonMediaTypeFormatter
            {
                SerializerSettings = new JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                }
            });
        }

        private static void MapRoutes(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "Api",
                routeTemplate: "api/{controller}",
                defaults: new { controller = "Browse" }
            );
        }

        private static void RegisterDependencies(HttpConfiguration config)
        {
            var container = new Container {Options = {DefaultScopedLifestyle = new WebApiRequestLifestyle()}};

            container.Register<IFileSystemService, FileSystemService>();
            container.Register<IBrowseService, BrowseService>();
            container.Register<IFileStatisticsService, FileStatisticsService>();

            container.RegisterWebApiControllers(config);

            container.Verify();

            config.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
        }
    }
}
