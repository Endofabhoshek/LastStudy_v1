
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
using LastStudy.Core.Helpers;
using System.Runtime.InteropServices.WindowsRuntime;
using LastStudy.Core.Flags;

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

        #region Institute
        [Route("create")]
        //[Authorize(Roles = "SuperAdmin")]
        public IHttpActionResult PosCreatetInstitute(InstituteDTO institute)
        {
            //NEED TO make this async and send the email upon confirmation
            var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(institute.InstituteCode).GenerateConnectionString();
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

        [Route("update")]
        public IHttpActionResult PostUpdateInstitute(InstituteDTO institute)
        {
            //NEED TO make this async and send the email upon confirmation
            var instituteBO = _serviceLocator.Resolve<IInstituteBO>(); //need a common method for this
            try
            {

                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(institute.InstituteCode).GenerateConnectionString(); //need a common method for this
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

        [Route("delete")]
        public IHttpActionResult PostDeleteInstitute([FromBody] string instituteCode)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(instituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                bool success = instituteBO.RemoveInstitute(instituteCode);

                if (success)
                {
                    return Ok("Institute Deleted Successfully");
                }
                else
                {
                    return BadRequest("Failed to delete institute.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("list")]
        public IHttpActionResult PostListAllInstitutes()
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                var connectionStrings = DatabaseHelper.GenerateConnectionString(""); //need a common method for this

                var institutes = instituteBO.GetAllInstitutes(connectionStrings);

                return Ok(institutes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region InstituteLocation
        [Route("institutelocation/create")]
        public IHttpActionResult PostCreateInstituteLocation(InstituteLocationDTO instituteLocationDTO)
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(instituteLocationDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.AddInstituteLocation(instituteLocationDTO) > 0)
                {
                    return Ok("Institute Location Added Successfully");
                }
                else
                {
                    return BadRequest("Institute Location creation failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("institutelocation/update")]
        public IHttpActionResult PostUpdateInstituteLocation(InstituteLocationDTO instituteLocationDTO)
        {
            //NEED TO make this async and send the email upon confirmation
            var instituteBO = _serviceLocator.Resolve<IInstituteBO>(); //need a common method for this
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(instituteLocationDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.EditInstituteLocation(instituteLocationDTO) > 0)
                {
                    return Ok("Institute Location Edited Successfully");
                }
                else
                {
                    return BadRequest("Institute Location Edit Failed ");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("institutelocation/delete")]
        public IHttpActionResult PostDeleteInstituteLocation(InstituteCodeWithIdDTO instituteCodeWithIdDTO)
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(instituteCodeWithIdDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                bool success = instituteBO.RemoveInstituteLocation(instituteCodeWithIdDTO.Id);

                if (success)
                {
                    return Ok("Institute Deleted Successfully");
                }
                else
                {
                    return BadRequest("Failed to delete institute.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("institutelocation/list")]
        public IHttpActionResult PostListAllInstituteLocations(InstituteCodeWithIdDTO instituteCodeWithIdDTO)
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                var connectionString = DatabaseHelper.GenerateConnectionString(instituteCodeWithIdDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);

                var institutes = instituteBO.GetAllInstituteLocations(instituteCodeWithIdDTO.Id);

                return Ok(institutes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Roles
        [Route("role/create")]
        public IHttpActionResult PostCreateRole(RoleDTO roleDTO)
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(roleDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.AddRole(roleDTO) > 0)
                {
                    return Ok("Role Created Successfully");
                }
                else
                {
                    return BadRequest("Role creation failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("role/update")]
        public IHttpActionResult PostUpdateRole(RoleDTO roleDTO)
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                string connectionString = DatabaseHelper.GenerateConnectionString(roleDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.UpdateRole(roleDTO) > 0)
                {
                    return Ok("Role Updated Successfully");
                }
                else
                {
                    return BadRequest("Role Updated failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("role/delete")]
        public IHttpActionResult PostDeleteRole(InstituteCodeWithIdDTO roleDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(roleDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                if (instituteBO.RemoveRole(roleDTO.Id))
                {
                    return Ok("Role Removed.");
                }
                else
                {
                    return BadRequest("Role Removal failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("role/list")]
        public IHttpActionResult PostListRoles(InstituteCodeWithIdDTO roleDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(roleDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                var courseGroups = instituteBO.ListRoles();

                if (courseGroups != null)
                {
                    return Ok(courseGroups);
                }
                else
                {
                    return BadRequest("Failed to get the role list");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region UserInvite
        [Route("user/invite/create")]
        public IHttpActionResult PostCreateInviteUser(UserInviteDTO userInviteDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(userInviteDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.SendUserInvite(userInviteDTO))
                {
                    return Ok("User Invited Successfully");
                }
                else
                {
                    return BadRequest("User Invitation Failed");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("user/invite/delete")]
        public IHttpActionResult PostDeleteInviteUser(UserInviteDTO userInviteDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(userInviteDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);
                if (instituteBO.RemoveUserInvite(userInviteDTO))
                {
                    return Ok("User Invitation Removed.");
                }
                else
                {
                    return BadRequest("User Invitation Removal failed");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("user/invite/list")]
        public IHttpActionResult PostListUserInvites(InstituteCodeWithIdDTO instituteCodeWithIdDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(instituteCodeWithIdDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);

                var listUserInvites = instituteBO.GetAllUserInvites();

                return Ok(listUserInvites);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region CourseGroup
        [Route("coursegroup/create")]
        public IHttpActionResult PostCreateCourseGroup(CourseGroupDTO courseGroupDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(courseGroupDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                if (instituteBO.AddCourseGroup(courseGroupDTO) > 0)
                {
                    return Ok("Course group added successfully.");
                }
                else
                {
                    return BadRequest("Course Group Creation failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("coursegroup/update")]
        public IHttpActionResult PostUpdateCourseGroup(CourseGroupDTO courseGroupDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(courseGroupDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                if (instituteBO.EditCourseGroup(courseGroupDTO) > 0)
                {
                    return Ok("Course group edited successfully.");
                }
                else
                {
                    return BadRequest("Course Group edit failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("coursegroup/delete")]
        public IHttpActionResult PostDeleteCourseGroup(InstituteCodeWithIdDTO courseGroupDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(courseGroupDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                if (instituteBO.RemoveCourseGroup(courseGroupDTO.Id))
                {
                    return Ok("Course Group Removed.");
                }
                else
                {
                    return BadRequest("Course group Removal failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("coursegroup/list")]
        public IHttpActionResult PostListCourseGroup(InstituteCodeWithIdDTO courseGroupDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(courseGroupDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                var courseGroups = instituteBO.ListCourseGroup();

                if (courseGroups != null)
                {
                    return Ok(courseGroups);
                }
                else
                {
                    return BadRequest("Failed to get the course group list");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region Course
        [Route("course/create")]
        public IHttpActionResult PostCreateCourse(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();

                string connectionString = DatabaseHelper.GenerateConnectionString(courseDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
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

        [Route("course/update")]
        public IHttpActionResult PostUpdateCourse(CourseDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(courseDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                if (instituteBO.EditCourse(courseDTO) > 0)
                {
                    return Ok("Course edited successfully.");
                }
                else
                {
                    return BadRequest("Course edit failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("course/delete")]
        public IHttpActionResult PostDeleteCourse(InstituteCodeWithIdDTO courseDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(courseDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                if (instituteBO.RemoveCourse(courseDTO.Id))
                {
                    return Ok("Course Removed.");
                }
                else
                {
                    return BadRequest("Course Removal failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("course/list")]
        public IHttpActionResult PostListCourse(string instituteCode)
        {
            try
            {
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(instituteCode).GenerateConnectionString(); //need a common method for this
                instituteBO.InitINS(connectionString);
                var courses = instituteBO.GetAllCourses(instituteCode);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region CourseGroupCourses
        [Route("coursegroupcourse/create")]
        public IHttpActionResult PostCreateCourseGroupCourse(CourseGroupDTO courseGroupDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Mandatory Fields not filled.");
                }
                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(courseGroupDTO.InstituteCode).GenerateConnectionString();
                instituteBO.InitINS(connectionString);

                if (instituteBO.AddCourseGroup(courseGroupDTO) > 0)
                {
                    return Ok("Course group added successfully.");
                }
                else
                {
                    return BadRequest("Course Group Creation failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

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
                string connectionString = DatabaseHelper.GenerateConnectionString(subjectDTO.InstituteCode).GenerateConnectionString(); //need a common method for this
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

        [Route("listsubject")]
        public IHttpActionResult PostListSubject(int courseID)
        {
            //try
            //{
            //    var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
            //    string connectionString = DatabaseHelper.GenerateConnectionString(instituteCode).GenerateConnectionString(); //need a common method for this
            //    instituteBO.InitINS(connectionString);
            //    var subjects = instituteBO.GetAllSubjects(courseID);
            //    return Ok(subjects);
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
            return null;
        }

        [Route("dropdownlist/states")]
        public IHttpActionResult PostStates()
        {
            try
            {
                var states = DatabaseHelper.States();
                return Ok(states);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dropdownlist/country")]
        public IHttpActionResult PostCountry()
        {
            try
            {//need to change this later
                var states = new List<string> { "India" };
                return Ok(states);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("dropdownlist/credits")]
        public IHttpActionResult PostCredit()
        {
            try
            {
                return Ok(Enum.GetNames(typeof(Credit)).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

