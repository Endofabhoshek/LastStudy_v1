using LastStudy.Core.Interfaces.DependencyInjector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LastStudy.Controllers
{
    public class BaseAPIController : ApiController
    {
        private readonly IServiceLocator _injector;

        public BaseAPIController(IServiceLocator injector)
        {
            this._injector = injector;
        }
    }
}
