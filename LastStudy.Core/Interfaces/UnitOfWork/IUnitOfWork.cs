using LastStudy.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        void InitINS(string connectionName);

        void InitLSDb();

        IInstituteRepository Insitutes { get; }
        IInstituteConnectionRepository InstituteConnections { get; }
        IUserInstituteRepository UserInstitutes { get; }
        IInstituteUserRepository InstituteUsers { get; }
        DbSet<TEntity> Set<TEntity>(string connectionName) where TEntity : class;
        ITeacherRepository Teachers { get; }
        IStudentRepository Students { get; }
        ICourseRepository Courses { get; }
        ICourseGroupRepository CourseGroups { get; }
        ISubjectRepository Subjects { get; }
        IInstituteRoleRepository InstituteRoles { get; }
        IInvitedUserRepository InvitedUsers { get; }
        IInstituteLocationRepository InstituteLocations { get; }
        ICourseGroupCourseRepository CourseGroupCourses { get; }
        ILSUserRepository LSUsers { get; }
        ILSInvitedUserRepository LSInvitedUsers { get; }
        IInstituteUserRoleRepository InstituteUserRoles { get; }
        DbSet<TModel> Collection<TModel>() where TModel : class;
        int SaveLS();
        int SaveINS();
        string InsDatabaseName();
    }
}
