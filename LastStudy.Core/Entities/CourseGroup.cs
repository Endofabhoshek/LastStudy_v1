using LastStudy.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Entities
{
    public class CourseGroup : IEntity
    {
        public int Id { get; set; }
        public string CourseGroupCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }        
        public Institute Institute { get; set; }
        public int InstituteId { get; set; }
    }
}
