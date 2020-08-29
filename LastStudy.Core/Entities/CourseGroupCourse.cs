using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class CourseGroupCourse : IEntity
    {
        public int Id { get; set; }
        public int CourseId { get; set; }
        public int CourseGroupId { get; set; }
        public CourseGroup CourseGroup { get; set; }
        public Course Course { get; set; }
    }
}
