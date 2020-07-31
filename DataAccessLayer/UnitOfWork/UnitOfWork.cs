using DataAccessLayer.DbContexts;
using LastStudy.Core.Interfaces.Repositories;
using LastStudy.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private INSDbContext _context;
        private string _connectionName;

        public IInsituteRepository Insitutes
        {
            get { return GetRepository<IInsituteRepository>(); }
        }

        private T GetRepository<T>()
        {
            Init();
        }

        public DbSet<TModel> Collection<TModel>() where TModel : class
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(string connectionName)
        {
            this._connectionName = connectionName;
            if (_context == null)
            {
                _context = new INSDbContext(connectionName);
            }
        }
    }
}
