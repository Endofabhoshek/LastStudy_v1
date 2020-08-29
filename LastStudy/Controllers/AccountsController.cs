using DataAccessLayer.DbContexts;
using LastStudy.Core.DTO;
using LastStudy.Core.Entities;
using LastStudy.Core.Helpers;
using LastStudy.Core.Interfaces.BOObjects;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.UnitOfWork;
using LastStudy.WebHelpers;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.UI.WebControls;

namespace LastStudy.Controllers
{
    [Authorize]
    [RoutePrefix("api/v1/accounts")]
    public class AccountsController : BaseAccountController
    {
        private readonly IServiceLocator _serviceLocator;
        public AccountsController(IServiceLocator serviceLocator) : base(serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

        #region Signup and login
        [AllowAnonymous]
        [Route("signup")]
        public async Task<IHttpActionResult> PostCreateLSUser(UserCreateDTO userCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var guid = Request.GetQueryNameValuePairs().FirstOrDefault(x => x.Key == "invitecode").Value;
                var existingUser = this.LSUserManager.FindByEmail(userCreate.Email);
                if (existingUser != null && guid != null)
                {
                    int output = AddInvitedUser(userCreate.Email, guid, existingUser.Id);
                    if (output > 0)
                    {
                        return Ok("Used Invited Successfully");
                    }
                    else
                    {
                        return BadRequest("User Invite failed");
                    }
                }
                else
                {
                    var user = new LSUser()
                    {
                        Email = userCreate.Email,
                        DateCreated = DateTime.Now,
                        UserName = userCreate.Email
                    };

                    IdentityResult addUserResult = await this.LSUserManager.CreateAsync(user, userCreate.Password);

                    if (addUserResult.Succeeded)
                    {

                        if (guid != null)
                        {
                            int output = AddInvitedUser(userCreate.Email, guid, existingUser.Id);
                            if (output > 0)
                            {
                                return Ok("User Created Successfully!");
                            }
                        }
                        return Ok("User Created Successfully");
                    }
                    else
                    {
                        return BadRequest(string.Join(",", addUserResult.Errors));
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int AddInvitedUser(string email, string guid, int userId)
        {
            using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
            {
                string instituteCode = unitOfWork.LSInvitedUsers.GetByGUID(guid).InstituteCode;

                var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
                string connectionString = DatabaseHelper.GenerateConnectionString(instituteCode).GenerateConnectionString();

                instituteBO.InitINS(connectionString);
                return instituteBO.AddUser(email,userId, guid);
            }
        }

        [AllowAnonymous]
        [Route("login")]
        public async Task<IHttpActionResult> PostLogin(UserCreateDTO loginUser)
        {
            var result = await this.LSUserManager.FindAsync(loginUser.UserName, loginUser.Password);

            if (result != null) // need to change to JWTand authenticate
            {
                //assign a context ot the user
                //need to add code to get the default institute code
                var jwtToken = JWTManager.GetToken(loginUser.UserName, result.Id);
                return Ok(new { token = jwtToken });
            }
            else
            {
                return BadRequest("Invaild Username or password");
            }
        }

        #endregion

        #region User Modification
        [Route("user/update")]
        public async Task<IHttpActionResult> PostUpdatetUser(UserEditDTO loginUser)
        {
            var result = await this.LSUserManager.FindByIdAsync(loginUser.Id);

            if (result != null) // need to change to JWTand authenticate
            {
                result.FullName = loginUser.FullName;
                result.AddressLine1 = loginUser.AddressLine1;
                result.AddressLine2 = loginUser.AddressLine2;
                result.City = loginUser.City;
                result.State = loginUser.State;
                result.Country = loginUser.Country;
                result.PinCode = loginUser.PinCode;
                result.PhoneNumber = loginUser.PhoneNumber;
                var successResult = await this.LSUserManager.UpdateAsync(result);
                if (successResult.Succeeded)
                {
                    return Ok("User Edited Successfully");
                }
                else
                {
                    return BadRequest("User edit failed");
                }
            }
            else
            {
                return BadRequest("User edit failed, an error occured");
            }
        }

        [Route("user/delete")]
        public async Task<IHttpActionResult> PostDeleteUser([FromBody] int userId)
        {
            var result = await this.LSUserManager.FindByIdAsync(userId);

            if (result != null) // need to change to JWTand authenticate
            {
                var successResult = await this.LSUserManager.DeleteAsync(result);
                if (successResult.Succeeded)
                {
                    return Ok("User Deleted Successfully");
                }
                else
                {
                    return BadRequest("User deletion failed");
                }
            }
            else
            {
                return BadRequest("User edit failed, an error occured");
            }
        }
        #endregion


        public async Task<IHttpActionResult> PostResetPassword(UserCreateDTO loginUser)
        {
            var user = await this.LSUserManager.FindByNameAsync(loginUser.UserName);
            IdentityResult result = null;
            if (user != null)
            {
                result = await this.LSUserManager.ResetPasswordAsync(user.Id, "", loginUser.Password);
            }
            if (result.Succeeded)
            {
                return Ok("Password reset success!");
            }
            else
            {
                return BadRequest("An error occured");
            }
        }



        [Route("adduser")]
        public async Task<IHttpActionResult> PostAddUser(UserCreateDTO userCreateDTO)
        {
            return null;
            //var instituteBO = _serviceLocator.Resolve<IInstituteBO>();
            //try
            //{
            //    if (!ModelState.IsValid)
            //    {
            //        return BadRequest("Mandatory Fields not filled.");
            //    }
            //    string connectionString = DatabaseHelper.GenerateConnectionString(userCreateDTO.InstituteCode).GenerateConnectionString();
            //    instituteBO.InitINS(connectionString);
            //    if (userCreateDTO.IsStudent)
            //    {
            //        var user = new Student()
            //        {
            //            FullName = userCreateDTO.FullName,
            //            Email = userCreateDTO.Email,
            //            DateCreated = DateTime.Now,
            //            UserName = userCreateDTO.Email
            //        };
            //        IdentityResult addUserResult = await this.LSUserManager.CreateAsync(user, userCreateDTO.Password);
            //        if (addUserResult.Succeeded)
            //        {
            //            using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
            //            {
            //                int instituteConnectionId = unitOfWork.InstituteConnections.GetByINSCode(userCreateDTO.InstituteCode).Id;
            //                UserInstitute userInstitute = new UserInstitute() { InstituteConnectionId = instituteConnectionId, LSUserId = user.Id };
            //                unitOfWork.UserInstitutes.Add(userInstitute);
            //                unitOfWork.SaveLS();
            //            }
            //        }
            //        return Ok("User added successfully.");
            //    }
            //    return Ok("User creation failed.");
            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }

        [Route("role/create")]
        public async Task<IHttpActionResult> PostAddRole(RoleDTO roleDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                LSRole role = new LSRole() { Description = roleDTO.Description, Name = roleDTO.Name };

                var roleResult = await this.LSRoleManager.CreateAsync(role);

                if (roleResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest(string.Join(",", roleResult.Errors));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("role/update")]
        public async Task<IHttpActionResult> PostEditRole(RoleDTO roleDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingrole = this.LSRoleManager.FindById(roleDTO.Id);

                if (existingrole != null)
                {
                    existingrole.Name = roleDTO.Name;
                    existingrole.Description = roleDTO.Description;
                    var roleResult = await this.LSRoleManager.UpdateAsync(existingrole);

                    if (roleResult.Succeeded)
                    {
                        return Ok("Role Edited Successfuly");
                    }
                    else
                    {
                        return BadRequest(string.Join(",", roleResult.Errors));
                    }
                }
                else
                {
                    return BadRequest("Role edit failed, an error occured");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("role/delete")]
        public IHttpActionResult PostDeleteRole([FromBody] int roleID)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var existingrole = this.LSRoleManager.FindById(roleID);

                if (existingrole != null)
                {

                    var roleResult = this.LSRoleManager.Delete(existingrole);

                    if (roleResult.Succeeded)
                    {
                        return Ok("Role Deleted Successfuly");
                    }
                    else
                    {
                        return BadRequest(string.Join(",", roleResult.Errors));
                    }
                }
                else
                {
                    return BadRequest("Role edit failed, an error occured");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("role/list")]
        public IHttpActionResult PostListRole()
        {
            try
            {
                return Ok(this.LSRoleManager.Roles.Select(x => new { x.Id, x.Name, x.Description }).ToList());

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[Route("user/invite/create")]
        //public async Task<IHttpActionResult> PostAddUserAfterInvite()
        //{

        //}
    }
}