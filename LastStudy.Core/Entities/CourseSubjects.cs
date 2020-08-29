using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class CourseSubjects : IEntity
    {
        public int Id { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public Subject Subject { get; set; }
        public int SubjectId { get; set; }
    }
}
