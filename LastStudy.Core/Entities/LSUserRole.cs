using LastStudy.Core.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class LSUserRole : IdentityUserRole<int>, IEntity
    {
        public int Id { get; set; }
        public LSUser LSUser { get; set; }
        public LSRole LSRole { get; set; }
    }
}
