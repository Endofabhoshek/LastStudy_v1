using LastStudy.Core.Entities;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CourseRepository : RepositoryWithId<Course>, ICourseRepository
    {
        public CourseRepository(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public List<Subject> GetCourses(int courseID)
        {
            throw new NotImplementedException();
        }
    }
}
