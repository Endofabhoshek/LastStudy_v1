//using Microsoft.AspNet.Identity.EntityFramework;
using LastStudy.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Data.Entity.Migrations.Infrastructure;
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
            modelBuilder.Entity<InstituteConnection>().Property(x => x.InstituteCode).HasMaxLength(255).IsRequired(); //need to make this column unique .HasIndex(x => x.InstituteCode).IsUnique()
            modelBuilder.Entity<InstituteConnection>().Property(x => x.DatabaseName).HasMaxLength(255).IsRequired();

            modelBuilder.Entity<LSUser>().ToTable("lsusers");
            modelBuilder.Entity<Teacher>().ToTable("teachers");
            modelBuilder.Entity<Student>().ToTable("students");
            modelBuilder.Entity<LSRole>().ToTable("lsroles");
            modelBuilder.Entity<LSUserRole>().HasKey(x => x.Id).ToTable("lsuser_roles");
            modelBuilder.Entity<LSUserLogin>().ToTable("lsuser_logins");
            modelBuilder.Entity<LSUserClaim>().ToTable("lsuser_claims");

            modelBuilder.Entity<UserInstitute>().ToTable("user_institutes");
        }
    }

    //[DbConfigurationType(typeof(MySqlEFConfiguration))]
    //public class INSDbContext : DbContext
    //{
    //    public INSDbContext(string connectionName) : base(connectionName)
    //    {

    //    }

    //    public INSDbContext() : base("TestInstitute") // pass 
    //    {

    //    }        

    //    #region Entity
    //    public DbSet<Institute> Institutes { get; set; }
    //    #endregion

    //    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);
    //        modelBuilder.Entity<Institute>().ToTable("institute_branches");

    //    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class INSDbContext : DbContext
    {
        public INSDbContext(string connectionName) : base(connectionName)
        {
            Database.SetInitializer(new CustomInitializer());
            Database.Initialize(true);
        }

        public INSDbContext() : base()
        {

        }
        public INSDbContext(DbConnection connection) : base(connection, true)
        {
            Database.SetInitializer(new CustomInitializer());
            Database.Initialize(true);
        }

        #region Entity
        public DbSet<Institute> Institutes { get; set; }
        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Institute>().ToTable("institute_branches");
            modelBuilder.Entity<Course>().ToTable("courses");
            modelBuilder.Entity<Subject>().ToTable("subjects");
        }
    }

    public class CustomInitializer : IDatabaseInitializer<INSDbContext>
    {
        public void InitializeDatabase(INSDbContext context)
        {
            try
            {
                if (!context.Database.Exists() || !context.Database.CompatibleWithModel(false))
                {
                    var configuration = new DbMigrationsConfiguration();
                    configuration.ContextType = typeof(INSDbContext);
                    configuration.AutomaticMigrationsEnabled = true;
                    configuration.MigrationsAssembly = typeof(INSDbContext).Assembly;
                    configuration.TargetDatabase = new DbConnectionInfo(context.Database.Connection.ConnectionString, "MySql.Data.MySqlClient");

                    var migrator = new DbMigrator(configuration);

                    migrator.Update();
                    //var migrations = migrator.GetPendingMigrations();
                    //if (migrations.Any())
                    //{
                    //    var scriptor = new MigratorScriptingDecorator(migrator);
                    //    string script = scriptor.ScriptUpdate(null, migrations.Last());
                    //    if (!String.IsNullOrEmpty(script))
                    //    {
                    //        context.Database.ExecuteSqlCommand(script);
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
