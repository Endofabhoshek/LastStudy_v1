using System;
using System.Threading.Tasks;
using System.Web.Http;
using DataAccessLayer.DbContexts;
using LastStudy.App_Start;
using LastStudy.IoC;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using SimpleInjector;
using SimpleInjector.Integration.WebApi;

//[assembly: OwinStartup(typeof(LastStudy.Startup))]

namespace LastStudy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Container container = LSRegisterInjector.CreateContainer();
            app.CreatePerOwinContext<LSDbContext>(LSDbContext.Create);
            app.CreatePerOwinContext<LSUserManager>(LSUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });

            HttpConfiguration webApiConfiguration = new HttpConfiguration();
            webApiConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            WebApiConfig.Register(webApiConfiguration);
            app.UseWebApi(webApiConfiguration);
        }
    }
}
