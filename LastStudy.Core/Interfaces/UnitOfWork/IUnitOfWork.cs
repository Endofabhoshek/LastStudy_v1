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

        IInsituteRepository Insitutes { get; }
        IInstituteConnectionRepository InsituteConnections { get; }
        IUserInstituteRepository UserInstitutes { get; }
        DbSet<TEntity> Set<TEntity>(string connectionName) where TEntity : class;

        DbSet<TModel> Collection<TModel>() where TModel : class;
        int Save();
    }
}
