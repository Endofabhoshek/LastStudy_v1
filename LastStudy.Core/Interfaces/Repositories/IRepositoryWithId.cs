using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Interfaces.Repositories
{
    public interface IRepositoryWithId<T> : IRepository<T> where T : class, IEntity
    {
        void GetById(int id);
        List<T> GetAll();
    }
}
