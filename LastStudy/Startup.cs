using System;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using System.Web.Http;
using DataAccessLayer.DbContexts;
using LastStudy.App_Start;
using LastStudy.IoC;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Jwt;
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

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = new PathString("/Account/Login"),
            //});

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Active,
                TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "http://localhost",
                    ValidAudience = "http://localhost",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("my_secret_key_12345"))
                }
            }); ;

            HttpConfiguration webApiConfiguration = new HttpConfiguration();
            webApiConfiguration.DependencyResolver = new SimpleInjectorWebApiDependencyResolver(container);
            WebApiConfig.Register(webApiConfiguration);
            app.UseWebApi(webApiConfiguration);
        }
    }
}
