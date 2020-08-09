using LastStudy.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http;
using LastStudy.Core.Interfaces.DependencyInjector;
using Microsoft.Owin.Security;
using Microsoft.AspNetCore.Http;

namespace LastStudy.Controllers
{
    public class BaseAccountController : BaseAPIController
    {
        private LSUserManager _LSUserManager = null;
        private LSRoleManager _LSRoleManager = null;
        public BaseAccountController(IServiceLocator injector) : base(injector)
        {

        }
        protected LSUserManager LSUserManager
        {
            get
            {
                return _LSUserManager ?? Request.GetOwinContext().GetUserManager<LSUserManager>();
            }
        }

        protected LSRoleManager LSRoleManager
        {
            get
            {
                return _LSRoleManager ?? Request.GetOwinContext().GetUserManager<LSRoleManager>();
            }
        }
        protected IAuthenticationManager AuthManager
        {
            get
            {
                return Request.GetOwinContext().Authentication;
            }
        }
    }
}
