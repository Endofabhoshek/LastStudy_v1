using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class CourseDTO : BaseDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int InstituteId { get; set; }
    }
}
