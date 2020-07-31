using DataAccessLayer.DbContexts;
using LastStudy.Core.DTO;
using LastStudy.Core.Entities;
using LastStudy.Core.Helpers;
using Microsoft.AspNet.Identity;
using System;
using System.Configuration;
using System.Threading.Tasks;
using System.Web.Http;

namespace LastStudy.Controllers
{
    [RoutePrefix("api/v1/accounts")]
    public class AccountsController : BaseApiController
    {
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(UserCreateDTO userCreate)
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
                    UserName = userCreate.Email, //need to change
                    IsInstituteAdmin = true
                };

                //if (userCreate.IsInstituteAdmin)
                //{
                //need to check if the institute code already exists
                //might add an email verification
                INSDbContext cont = new INSDbContext(DatabaseHelper.GenerateConnectionString(ConfigurationManager.AppSettings["server"], ConfigurationManager.AppSettings["username"], ConfigurationManager.AppSettings["password"], userCreate.InstituteCode));
                //}
                //else
                //{
                //    //ask for institute code
                //    //and then send for an approval
                //}

                IdentityResult addUserResult = await this.LSUserManager.CreateAsync(user, userCreate.Password);

                if (addUserResult.Succeeded)
                {
                    //add the code to enter the details in the institute_connections
                    return Ok("Success");
                }
                else
                {
                    return Ok("Failure");
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Route("login")]
        public async Task<IHttpActionResult> Login(UserCreateDTO loginUser)
        {
            var result = await this.LSUserManager.FindAsync(loginUser.UserName, loginUser.Password);

            if (result != null) // need to change to JWTand authenticate
            {
                //assign a context ot the user
                return Ok("Success");
            }
            else
            {
                return Ok("Failure");
            }
        }
    }
}
