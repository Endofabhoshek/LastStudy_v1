using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class InstituteConnection : IEntity
    {
        public int Id { get; set; }
        public string InstituteCode { get; set; }
        public string DatabaseName { get; set; } // since its a single server
        public bool IsActive { get; set; }
    }
}
