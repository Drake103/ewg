using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using EWG.DependencyResolver;
using EWG.Frontend.Security;
using EWG.Web.Controllers;

namespace EWG.Web
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            IocConfiguration();
        }

        protected void Application_BeginRequest()
        {
            var res = HttpContext.Current.Response;
            var req = HttpContext.Current.Request;
            res.AddHeader("Access-Control-Allow-Methods", "POST, PUT, OPTIONS");
            res.AddHeader("Access-Control-Allow-Origin", "*");
            res.AddHeader("Access-Control-Allow-Headers", "Content-Type");

            if (req.HttpMethod == "OPTIONS")
            {
                res.StatusCode = 200;
                res.Flush();
            }
        }

        private void IocConfiguration()
        {
            var builder = new AutofacContainerBuilder(null).Get();

            builder.RegisterControllers(typeof (ReplaysController).Assembly);

            RegisterSecurity(builder);

            var container = builder.Build();

            System.Web.Mvc.DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            /*var dbService = container.Resolve<IDatabaseService>();
            dbService.DropCreateAndInit(null);*/
        }

        private void RegisterSecurity(ContainerBuilder builder)
        {
            builder.RegisterType<ConfigFileAuthenticationConfiguration>().AsImplementedInterfaces();
            builder.RegisterType<CookieAuthenticator>().AsImplementedInterfaces();
            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>();
        }
    }
}
