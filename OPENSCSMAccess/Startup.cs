using System.Web.Http;
using Owin;

namespace OPENSCSMAccessM
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HttpConfiguration();  

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}",
                defaults: new { action = RouteParameter.Optional }
            );

            app.UseWebApi(config);
        }
    }
}