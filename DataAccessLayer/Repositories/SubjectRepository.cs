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
    public class SubjectRepository : RepositoryWithId<Subject>, ISubjectRepository
    {
        public SubjectRepository(IServiceLocator serviceLocator) : base(serviceLocator)
        {
        }

        public IQueryable<Subject> GetSubjects(int courseID)
        {
            return this._records.Where(x => x.CourseId == courseID);
        }
    }
}
