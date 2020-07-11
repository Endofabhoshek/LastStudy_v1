using System;
using System.Threading.Tasks;
using DataAccessLayer.DbContexts;
using LastStudy.App_Start;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

//[assembly: OwinStartup(typeof(LastStudy.Startup))]

namespace LastStudy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<LSDbContext>(LSDbContext.Create);
            app.CreatePerOwinContext<LSUserManager>(LSUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}
