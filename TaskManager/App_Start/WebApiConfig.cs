using StructureMap;
using Swashbuckle.Application;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using TaskManager.API.DependencyResolution;

namespace TaskManager
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API routes
            config.MapHttpAttributeRoutes();

            //Set swagger as the welcome page
            config.Routes.MapHttpRoute(
            name: "swagger_root",
            routeTemplate: "",
            defaults: null,
            constraints: null,
            handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger"));

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
