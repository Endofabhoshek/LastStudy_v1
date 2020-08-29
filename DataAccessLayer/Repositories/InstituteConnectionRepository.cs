using LastStudy.Core.Entities;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class InstituteConnectionRepository : RepositoryWithId<InstituteConnection>, IInstituteConnectionRepository
    {
        public InstituteConnectionRepository(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public bool FindByINSCode(string instituteCode)
        {
            if (this._records.Where(x => x.InstituteCode == instituteCode && x.IsActive).Count() > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public List<InstituteConnection> GetActiveInstitutes()
        {
            return this._records.Where(x => x.IsActive).ToList();
        }

        public InstituteConnection GetByINSCode(string instituteCode)
        {
            return _records.FirstOrDefault(x => x.InstituteCode == instituteCode && x.IsActive);
        }
    }
}
