using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class UserCreateDTO : BaseDTO
    {
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        //Need to check this
        public bool IsInstituteAdmin { get; set; }
        public string UserName { get; set; }
        public bool IsTeacher { get; set; }
        public bool IsStudent { get; set; }
    }
}
