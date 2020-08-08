
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
using LastStudy.Core.Interfaces.BOObjects;
using Microsoft.AspNet.Identity;
using LastStudy.App_Start;
using Microsoft.AspNet.Identity.Owin;

namespace LastStudy.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/institute")]
    public class InstituteController : BaseAPIController
    {
        protected LSUserManager LSUserManager
        {
            get
            {
                return LSUserManager ?? Request.GetOwinContext().GetUserManager<LSUserManager>();
            }
        }
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
        public async Task<IHttpActionResult> AddUser(UserCreateDTO userCreateDTO)
        {
            var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(userCreateDTO.InstituteCode);
                instituteBO.InitINS(connectionString);
                if (userCreateDTO.IsStudent)
                {
                    var user = new Student()
                    {
                        FullName = userCreateDTO.FullName,
                        Email = userCreateDTO.Email,
                        DateCreated = DateTime.Now,
                        UserName = userCreateDTO.Email
                    };
                    IdentityResult addUserResult = await this.LSUserManager.CreateAsync(user, userCreateDTO.Password);
                }
                //var user = new LSUser()
                //{
                //    FullName = userCreateDTO.FullName,
                //    Email = userCreateDTO.Email,
                //    DateCreated = DateTime.Now,
                //    UserName = userCreateDTO.Email
                //};

                
                //if (addUserResult.Succeeded)
                //{
                //    if (instituteBO.AddUser(userCreateDTO, user.Id) > 0)
                //    {
                //        return Ok("User Successfully Created");
                //    }
                //    else
                //    {
                //        return BadRequest("User creation failed / Already exists");
                //    }
                //}
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("");
        }

        [Route("create")]
        public IHttpActionResult PostInstitute(InstituteDTO institute)
        {
            //NEED TO make this async and send the email upon confirmation
            var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
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
                    instituteBO.InitINS(connectionString);
                    if (instituteBO.AddInstitute(institute) > 0)
                    {
                        return Ok("Institute Created Successfully");
                    }
                    else
                    {
                        return BadRequest("Institute Creation Failed / Already exists");
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
