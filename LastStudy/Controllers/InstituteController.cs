
using DataAccessLayer.DbContexts;
using LastStudy.Core.DTO;
using LastStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Configuration;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.UnitOfWork;
using LastStudy.WebHelpers;

namespace LastStudy.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/institute")]
    public class InstituteController : BaseAPIController
    {
        private readonly IServiceLocator _serviceLocator;
        public InstituteController(IServiceLocator serviceLocator) : base(serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }
        //private InstituteRepository instituteRepository;

        //[Route("add")]
        //public async Task<IHttpActionResult> Add(Institute institute)
        //{
        //    instituteRepository = new InstituteRepository("TestInstitute");

        //    instituteRepository.InsertInstitute(institute);
        //    int result = instituteRepository.Save();
        //    if (result >= 0) //
        //    {
        //        return Ok("Success");
        //    }
        //    else
        //    {
        //        return Ok("Failure");
        //    }
        //}

        [Route("adduser")]
        public async Task<IHttpActionResult> AddUser()
        {
            //authenticate the role with the database
            //send the email with database context
            return Ok("");
        }

        [Route("create")]
        public IHttpActionResult PostInstitute(InstituteDTO institute)
        {
            //NEED TO make this async and send the email upon confirmation
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(institute.InstituteCode);
                INSDbContext context = new INSDbContext(connectionString);
                if (context != null)
                {
                    
                    using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                    {
                        InstituteConnection insconnection = new InstituteConnection() { DatabaseName = institute.InstituteCode, InstituteCode = institute.InstituteCode };
                        unitOfWork.InsituteConnections.Add(insconnection);
                        unitOfWork.Save();

                        UserInstitute userInstitute = new UserInstitute() { LSUserId = institute.UserId, InstituteConnectionId = insconnection.Id };
                        unitOfWork.UserInstitutes.Add(userInstitute);
                        unitOfWork.Save();
                    }
                    return Ok("Institute Created Successfully");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
            
        }
    }
}
