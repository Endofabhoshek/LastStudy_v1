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
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int UserId { get; set; }
        //[Phone]
        public string PhoneNumber { get; set; }
        //[Phone]
        public string MobileNumber { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public DateTime FoundedYear { get; set; }
    }
}
