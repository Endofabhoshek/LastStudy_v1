using DataAccessLayer.DbContexts;
using LastStudy.Core.DTO;
using LastStudy.Core.Entities;
using LastStudy.Core.Interfaces.BOObjects;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.UnitOfWork;
using LastStudy.WebHelpers;
using Microsoft.AspNet.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;

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

        [AllowAnonymous]
        [Route("create")]
        public async Task<IHttpActionResult> PostUser(UserCreateDTO userCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var user = new LSUser()
                {
                    FullName = userCreate.FullName,
                    Email = userCreate.Email,
                    DateCreated = DateTime.Now,
                    UserName = userCreate.Email
                };

                //if (userCreate.IsInstituteAdmin)
                //{
                //need to check if the institute code already exists
                //might add an email verification
                //INSDbContext cont = new INSDbContext(DatabaseHelper.GenerateConnectionString(ConfigurationManager.AppSettings["server"], ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"], userCreate.InstituteCode));
                //}
                //else
                //{
                //    //ask for institute code
                //    //and then send for an approval
                //}

                IdentityResult addUserResult = await this.LSUserManager.CreateAsync(user, userCreate.Password);

                if (addUserResult.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    //Might use a common method in the BaseAPIController
                    return BadRequest("An Error Occured");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [AllowAnonymous]
        [Route("login")]
        public async Task<IHttpActionResult> Login(UserCreateDTO loginUser)
        {
            var result = await this.LSUserManager.FindAsync(loginUser.UserName, loginUser.Password);

            if (result != null) // need to change to JWTand authenticate
            {
                //assign a context ot the user
                //need to add code to get the default institute code
                var jwtToken = JWTManager.GetToken(loginUser.UserName, result.Id);
                return Ok(jwtToken);
            }
            else
            {
                return BadRequest("Invaild Username or password");
            }
        }

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

        [Route("edit")]
        public async Task<IHttpActionResult> PostEdit(UserCreateDTO loginUser)
        {
            var result = await this.LSUserManager.FindByIdAsync(loginUser.Id);

            if (result != null) // need to change to JWTand authenticate
            {
                result.FullName = loginUser.FullName;
                result.Email = loginUser.Email;
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

        [Route("adduser")]
        public async Task<IHttpActionResult> PostAddUser(UserCreateDTO userCreateDTO)
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
                    if (addUserResult.Succeeded)
                    {
                        using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                        {
                            int instituteConnectionId = unitOfWork.InsituteConnections.GetByINSCode(userCreateDTO.InstituteCode).Id;
                            UserInstitute userInstitute = new UserInstitute() { InstituteConnectionId = instituteConnectionId, LSUserId = user.Id };
                            unitOfWork.UserInstitutes.Add(userInstitute);
                            unitOfWork.SaveLS();
                        }
                    }
                    return Ok("User added successfully.");
                }
                return Ok("User creation failed.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
