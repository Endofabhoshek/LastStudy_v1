
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
using System.Diagnostics;

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

        [Route("edit")]
        public IHttpActionResult PostEditInstitute(InstituteDTO institute)
        {
            //NEED TO make this async and send the email upon confirmation
            var instituteBO = _serviceLocator.Resolve<IInstituteBO>(); //need a common method for this
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(institute.InstituteCode); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.EditInstitute(institute) > 0)
                {
                    return Ok("Institute Edited Successfully");
                }
                else
                {
                    return BadRequest("Institute Edit Failed ");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("addcourse")]
        public IHttpActionResult PostCreateCourse(CourseDTO courseDTO)
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(courseDTO.InstituteCode); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.AddCourse(courseDTO) > 0)
                {
                    return Ok("Course Created Successfully");
                }
                else
                {
                    return BadRequest("Course creation failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("addsubject")]
        public IHttpActionResult PostCreateSubject(SubjectDTO subjectDTO)
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(subjectDTO.InstituteCode); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.AddSubject(subjectDTO) > 0)
                {
                    return Ok("Subject Created Successfully");
                }
                else
                {
                    return BadRequest("Subject creation failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

