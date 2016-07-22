using System.Web.Http;
using WebActivatorEx;
using Swashbuckle.Application;
using Swashbuckle.Swagger;
using System.Web.Http.Description;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration.EnableSwagger(c =>
                {
                    c.SingleApiVersion("v1", "TaskManager.API");
                    c.IncludeXmlComments(string.Format(@"{0}\bin\TaskManager.API.XML", System.AppDomain.CurrentDomain.BaseDirectory));
                    c.DocumentFilter<AuthTokenOperation>();
                }).EnableSwaggerUi(c => {
                    c.InjectJavaScript(thisAssembly, "TaskManager.API.SwaggerExtensions.onComplete.js");
                });
        }

    }

    class AuthTokenOperation : IDocumentFilter
    {
        public void Apply(SwaggerDocument swaggerDoc, SchemaRegistry schemaRegistry, IApiExplorer apiExplorer)
        {
            swaggerDoc.paths.Add("/api/Account/LoginToken", new PathItem
            {
                post = new Operation
                {
                    tags = new List<string> { "Account" },
                    consumes = new List<string>
                            {
                                "application/x-www-form-urlencoded"
                            },
                    parameters = new List<Parameter> {
                            new Parameter
                            {
                                type = "string",
                                name = "grant_type",
                                required = true,
                                @in = "formData"
                            },
                            new Parameter
                            {
                                type = "string",
                                name = "username",
                                required = false,
                                @in = "formData"
                            },
                            new Parameter
                            {
                                type = "string",
                                name = "password",
                                required = false,
                                @in = "formData"
                            }
                        }
                }
            });
        }
    }
}