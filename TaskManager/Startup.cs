using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using StructureMap;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using TaskManager.API.DependencyResolution;
using TaskManager.API.Providers;
using TaskManager.Core.BusinessServices;

[assembly: OwinStartup(typeof(TaskManager.API.Startup))]
namespace TaskManager.API
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();
            ConfigureOAuth(app, config);

            WebApiConfig.Register(config);
            app.UseWebApi(config);

            SwaggerConfig.Register();
        }

        public void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            //Dependency Injection
            IContainer container = new Container(c => c.AddRegistry<BootstrapperRegistry>());
            config.Services.Replace(
                typeof(IHttpControllerActivator),
                new StructureMapWebApiControllerActivator(container));

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/api/Account/LoginToken"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SampleAuthorizationServerProvider(container.GetInstance<IAuthenticationService>())
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }

    }
}