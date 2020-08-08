using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class BaseDTO
    {
        [Required(AllowEmptyStrings = true)]
        public string InstituteCode { get; set; }
    }
}
