using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : class
    {
        void Init();
        IQueryable<T> ForJoin();
        void Add(T entity);
    }
}
