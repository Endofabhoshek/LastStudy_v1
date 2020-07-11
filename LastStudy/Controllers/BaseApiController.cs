using LastStudy.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Web.Http;

namespace LastStudy.Controllers
{
    public class BaseApiController : ApiController
    {
        private LSUserManager _LSUserManager = null;
        public BaseApiController()
        {

        }
        protected LSUserManager LSUserManager
        {
            get
            {
                return _LSUserManager ?? Request.GetOwinContext().GetUserManager<LSUserManager>();
            }
        }
    }
}
