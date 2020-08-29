using LastStudy.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Interfaces.Repositories
{
    public interface IInvitedUserRepository : IRepositoryWithId<InvitedUser>
    {
        InvitedUser GetUnregisteredByEmail(string email);
        InvitedUser GetByEmailGuid(string email, string guid);
    }
}
