using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class Institute : IEntity // can be branches
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHeadBranch { get; set; }
        public int ParentBranchId { get; set; } // for branches inside branch
        public bool IsParentBranch { get; set; } // is parent
        public string Address { get; set; } // Need to add many properties here
        public string CIty { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }

    }
}
