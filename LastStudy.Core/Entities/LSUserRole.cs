using LastStudy.Core.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class LSUserRole : IdentityUserRole<int>, IEntity
    {
        public int Id { get; set; }           
        public virtual LSUser LSUser { get; set; }
        public virtual LSRole LSRole { get; set; }
    }
}
