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
    public class LSInvitedUserRepository : RepositoryWithId<LSInvitedUser>, ILSInvitedUserRepository
    {
        public LSInvitedUserRepository(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public LSInvitedUser GetByGUID(string guid)
        {
            return this._records.FirstOrDefault(x => x.Guid == guid);
        }
    }
}
