//using Microsoft.AspNet.Identity.EntityFramework;
using LastStudy.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbContext
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LSDbContext : IdentityDbContext<LSUser, LSRole, int, LSUserLogin, LSUserRole, LSUserClaim>
    {
        public LSDbContext() : base("LSBaseDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LSUser>().ToTable("lsusers");
            modelBuilder.Entity<LSRole>().ToTable("lsroles");
            modelBuilder.Entity<LSUserRole>().HasKey(x => x.Id).ToTable("lsuser_roles");
            modelBuilder.Entity<LSUserLogin>().ToTable("lsuser_logins");
            modelBuilder.Entity<LSUserClaim>().ToTable("lsuser_claims");
        }
    }
}
