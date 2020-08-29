using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class InstituteLocation : IEntity
    {
        public int Id { get ; set ; }
        public string GstNumber { get; set; }
        public string BranchCode { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public Institute Institute { get; set; }
        public int InstituteId { get; set; }
        public bool PrimaryBranch { get; set; }
    }
}
