using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : class, IEntity
    {
        void Init(string connectionName);
        IQueryable<T> Records();
        void Add(T entity);
    }
}
