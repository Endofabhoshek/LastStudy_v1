using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class InstituteLocationDTO : BaseDTO
    {
        public int Id { get; set; }
        public string GstNumber { get; set; }
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
        [Required]
        public int InstituteId { get; set; }
    }
}
