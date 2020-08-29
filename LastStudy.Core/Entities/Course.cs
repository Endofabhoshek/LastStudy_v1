using LastStudy.Core.Flags;
using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class Course : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CourseCode { get; set; }
        public decimal CourseAmount { get; set; }
        public Credit Credit { get; set; }
    }
}
