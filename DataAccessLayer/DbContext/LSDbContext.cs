//using Microsoft.AspNet.Identity.EntityFramework;
using LastStudy.Core.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.DbContext
{
    public class LSDbContext : IdentityDbContext<LSUser>
    {
        public LSDbContext() : base("LSBaseDb")
        {

        }
    }
}
