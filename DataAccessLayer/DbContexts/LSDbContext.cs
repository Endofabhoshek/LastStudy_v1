//using Microsoft.AspNet.Identity.EntityFramework;
using LastStudy.Core.Entities;
using LastStudy.Core.Helpers;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbContexts
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class LSDbContext : IdentityDbContext<LSUser, LSRole, int, LSUserLogin, LSUserRole, LSUserClaim>
    {
         DbSet<InstituteConnection> InstituteConnections { get; set; }
        public static LSDbContext Create()
        {
            return new LSDbContext();
        }
        public LSDbContext() : base("LSBaseDb")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InstituteConnection>().ToTable("institute_connections");
            modelBuilder.Entity<InstituteConnection>().Property(x => x.InstituteCode).IsRequired();
            modelBuilder.Entity<InstituteConnection>().Property(x => x.DatabaseName).IsRequired();

            modelBuilder.Entity<LSUser>().ToTable("lsusers");
            modelBuilder.Entity<LSUser>().HasRequired(x => x.Institute).WithMany().HasForeignKey(t => t.InstituteConnectionId).WillCascadeOnDelete(true);

            modelBuilder.Entity<LSRole>().ToTable("lsroles");
            modelBuilder.Entity<LSUserRole>().HasKey(x => x.Id).ToTable("lsuser_roles");
            modelBuilder.Entity<LSUserLogin>().ToTable("lsuser_logins");
            modelBuilder.Entity<LSUserClaim>().ToTable("lsuser_claims");
        }
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class INSDbContext : DbContext
    {
        public INSDbContext(string connectionName) : base(connectionName)
        {

        }

        public INSDbContext() : base("TestInstitute") // pass 
        {

        }

        #region Entity
        public DbSet<Institute> Institutes { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Institute>().ToTable("institute_branches");

        }
    }
}
