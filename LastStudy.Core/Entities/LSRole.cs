using LastStudy.Core.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class LSRole : IdentityRole<int, LSUserRole>, IEntity
    {
        public string Description { get; set; }
    }
}
