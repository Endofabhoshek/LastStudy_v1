using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class LSInvitedUser : IEntity
    {
        public int Id { get; set; }
        public string InstituteCode { get; set; }
        public string Guid { get; set; }
    }
}
