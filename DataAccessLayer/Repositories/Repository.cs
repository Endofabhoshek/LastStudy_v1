using LastStudy.Core.Interfaces;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.Repositories;
using LastStudy.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly IServiceLocator _serviceLocator;
        protected IUnitOfWork _unitOfWork;
        protected DbSet<T> _records;

        public Repository(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

        public void Add(T entity)
        {
            this._records.Add(entity);
        }

        public void Dispose()
        {
            if (this._unitOfWork != null)
            {
                this._unitOfWork.Dispose();
                this._unitOfWork = null;
            }
        }

        public void Init(string connectionName)
        {
            this._unitOfWork = this._serviceLocator.Resolve<IUnitOfWork>();
            this._records = this._unitOfWork.Set<T>(connectionName);
        }

        public IQueryable<T> Records()
        {
            throw new NotImplementedException();
        }
    }
}
