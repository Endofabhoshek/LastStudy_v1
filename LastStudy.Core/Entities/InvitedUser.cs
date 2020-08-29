using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class InvitedUser : IEntity
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public int InstituteRoleId { get; set; }
        public InstituteRole InstituteRole { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public bool IsRegistered { get; set; }
        public string Message { get; set; }
        public string Guid { get; set; }
    }
}
