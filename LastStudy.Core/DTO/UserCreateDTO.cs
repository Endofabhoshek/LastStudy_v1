using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class UserCreateDTO
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool IsInstituteAdmin { get; set; }
        public string UserName { get; set; }
        public string InstituteCode { get; set; }
        
    }
}
