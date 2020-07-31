using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class UserInstitutes : IEntity
    {
        public int Id { get ; set ; }
        public LSUser LSUser { get; set; }
        public int LSUserId { get; set; }
        public InstituteConnection InstituteConnection { get; set; }
        public int InstituteConnectionId { get; set; }
    }
}
