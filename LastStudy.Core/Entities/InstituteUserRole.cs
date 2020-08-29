using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class InstituteUserRole : IEntity
    {
        public int Id { get ; set; }
        public int InstituteRoleId { get; set; }
        public int InstituteUserId { get; set; }
        public InstituteRole InstituteRole { get; set; }
        public InstituteUser InstituteUser { get; set; }
    }
}
