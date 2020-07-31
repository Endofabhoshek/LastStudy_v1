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
        void Init(string connectionName);

        IInsituteRepository Insitutes { get; }
        DbSet<TModel> Collection<TModel>() where TModel : class;
    }
}
