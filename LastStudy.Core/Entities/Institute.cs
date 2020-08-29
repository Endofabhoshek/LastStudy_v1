using LastStudy.Core.Flags;
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
        public InstituteTypes InstituteType { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }        
        public string InstituteCode { get; set; }        
        public bool IsHeadBranch { get; set; }
        public int ParentBranchId { get; set; } // for branches inside branch
        public bool IsParentBranch { get; set; } // is parent        
        public string PanNumber { get; set; }
        public DateTime DateCreated { get; set; }
        public int CreatedByUserId { get; set; }
        public ICollection<Course> Courses { get; set; }
        //public DateTime FoundedYear { get; set; }
    }
}
