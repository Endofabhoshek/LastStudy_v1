using LastStudy.Core.DTO;
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
    public class InstituteLocationRepository : RepositoryWithId<InstituteLocation>, IInstituteLocationRepository
    {
        public InstituteLocationRepository(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public List<InstituteLocation> GetAllByInstitute(int instituteID)
        {
            return this._records.Where(x => x.InstituteId == instituteID).ToList();
        }
    }
}
