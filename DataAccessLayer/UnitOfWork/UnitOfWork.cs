using DataAccessLayer.DbContexts;
using LastStudy.Core.Interfaces.Repositories;
using LastStudy.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private INSDbContext _context;

        public IInsituteRepository Institues => throw new NotImplementedException();

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(string connectionName)
        {
            if (_context == null)
            {
                _context = new INSDbContext(connectionName);
            }
        }
    }
}
