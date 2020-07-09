using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(LastStudy.Startup))]

namespace LastStudy
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

        }
    }
}
