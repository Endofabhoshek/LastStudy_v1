using DataAccessLayer.DbContexts;
using LastStudy.Core.Entities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LastStudy.App_Start
{
    public class IdentityConfig
    {
    }

    public class LSUserManager : UserManager<LSUser, int>
    {
        public LSUserManager(IUserStore<LSUser, int> store) : base(store)
        {

        }

        public static LSUserManager Create(IdentityFactoryOptions<LSUserManager> options, IOwinContext owincontext)
        {
            var manager = new LSUserManager(new UserStore<LSUser, LSRole, int, LSUserLogin, LSUserRole, LSUserClaim>(new LSDbContext()));

            manager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireDigit = true,
                RequireLowercase = true,
                RequireUppercase = true,
                RequireNonLetterOrDigit = true
            };            

            return manager;
        }
    }
}