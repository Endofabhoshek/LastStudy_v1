using DataAccessLayer.Repositories;
using LastStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace LastStudy.Controllers
{
    [RoutePrefix("api/v1/institute")]
    public class InstituteController : ApiController
    {
        private InstituteRepository instituteRepository;

        [Route("add")]
        public async Task<IHttpActionResult> Add(Institute institute)
        {
            instituteRepository = new InstituteRepository("TestInstitute");

            instituteRepository.InsertInstitute(institute);
            int result = instituteRepository.Save();
            if (result >= 0) //
            {
                return Ok("Success");
            }
            else
            {
                return Ok("Failure");
            }
        }
    }
}
