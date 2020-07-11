using LastStudy.Core.DTO;
using LastStudy.Core.Entities;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new LSUser()
            {
                FullName = userCreate.FullName,
                Email = userCreate.Email,
                DateCreated = DateTime.Now,
                UserName = userCreate.Email //need to change
            };

            IdentityResult addUserResult = await this.LSUserManager.CreateAsync(user, userCreate.Password);
            if (userCreate.IsInstituteAdmin)
            {
                //do email verification
                //and create database and then add the user as institute admin
            }
            else
            {
                //ask for institute code
                //and then send for an approval
            }
            if (addUserResult.Succeeded)
            {
                return Ok("Success");
            }
            else
            {
                return Ok("Failure");
            }
        }

        [Route("login")]
        public  async Task<IHttpActionResult> Login(UserCreateDTO loginUser)
        {
            var result = await this.LSUserManager.FindAsync(loginUser.UserName, loginUser.Password);

            if (result != null) // need to change to JWTand authenticate
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
