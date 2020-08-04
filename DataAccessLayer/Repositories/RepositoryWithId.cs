using LastStudy.Core.Interfaces;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class RepositoryWithId<T> : Repository<T>, IRepositoryWithId<T> where T : class, IEntity
    {
        public RepositoryWithId(IServiceLocator serviceLocator)
           : base(serviceLocator)
        {
        }
        public List<T> GetAll()
        {
            return this._records.ToList();
        }

        public T GetById(int id)
        {
            T entity = this._records.FirstOrDefault(x => x.Id == id);
            return entity;
        }
    }
}
