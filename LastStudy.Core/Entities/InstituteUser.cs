using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class InstituteUser : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
    }
}
