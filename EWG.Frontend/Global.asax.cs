using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using EWG.DependencyResolver;
using EWG.Frontend.Controllers;
using EWG.Frontend.Security;
using EWG.Infrastructure.Services.Common;

namespace EWG.Frontend
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        private const string NhConfigurationFilePath = "hibernate.cfg.xml";

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            IocConfiguration();
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