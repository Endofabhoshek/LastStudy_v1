using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.DTO
{
    public class CourseGroupCourseDTO : BaseDTO
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int CourseGroupId { get; set; }
        public string CourseGroup { get; set; }
        public string Course { get; set; }
    }
}
