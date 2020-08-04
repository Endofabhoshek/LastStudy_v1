using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class InstituteDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string InstituteCode { get; set; }
        public string Address { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public int UserId { get; set; }
    }
}
