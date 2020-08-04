using DataAccessLayer.DbContexts;
using LastStudy.Core.DTO;
using LastStudy.Core.Entities;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.WebHelpers;
using Microsoft.AspNet.Identity;
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
        private readonly IServiceLocator _injector;
        public AccountsController(IServiceLocator injector) : base(injector)
        {
            this._injector = injector;
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
                var jwtToken = JWTManager.GetToken(loginUser.UserName, result.Id);
                return Ok(jwtToken);
            }
            else
            {
                return BadRequest("Invaild Username or password");
            }
        }
    }
}
