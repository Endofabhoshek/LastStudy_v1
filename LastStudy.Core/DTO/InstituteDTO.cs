using LastStudy.Core.Flags;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class InstituteDTO : BaseDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public InstituteTypes InstituteType { get; set; }
        public string ContactNumber { get; set; }
        public string Email { get; set; }
        public string GstNumber { get; set; }
        public string PanNumber { get; set; }
        [Required]
        public string BranchCode { get; set; }
        [Required]
        public string AddressLine1 { get; set; }
        [Required]
        public string AddressLine2 { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        public string PinCode { get; set; }
        public int UserId { get; set; }
        public int InstituteLocationId { get; set; }
    }
}
