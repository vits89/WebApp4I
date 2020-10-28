using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.ModelBinding.Binders;
using WebApp4I.WebApp.Infrastructure;
using WebApp4I.WebApp.Models;

namespace WebApp4I.WebApp
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Конфигурация и службы веб-API
            config.Services.Insert(typeof(ModelBinderProvider), 0,
                new SimpleModelBinderProvider(typeof(IEnumerable<PostedFileModel>), new PostedFilesModelBinder()));

            // Маршруты веб-API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
