using LastStudy.Core.Entities;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ubiety.Dns.Core.Records;

namespace DataAccessLayer.Repositories
{
    public class InvitedUserRepository : RepositoryWithId<InvitedUser>, IInvitedUserRepository
    {
        public InvitedUserRepository(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public InvitedUser GetByEmailGuid(string email, string guid)
        {
            return this._records.FirstOrDefault(x => x.Email == email && x.Guid == guid);
        }

        public InvitedUser GetUnregisteredByEmail(string email)
        {
            return this._records.FirstOrDefault(x => x.Email == email && x.IsRegistered == false);
        }
    }
}
