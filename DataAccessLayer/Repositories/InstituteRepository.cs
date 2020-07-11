using DataAccessLayer.DbContexts;
using LastStudy.Core.Entities;
using LastStudy.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class InstituteRepository : IInsituteRepository
    {
        private INSDbContext _context;

        public InstituteRepository(string connection)
        {
            _context = new INSDbContext(connection);
        }

        public IEnumerable<Institute> GetStudents()
        {
            return _context.Institutes.ToList();
        }

        public void InsertInstitute(Institute institute)
        {
            _context.Institutes.Add(institute);
        }

        public int Save()
        {
           return _context.SaveChanges();
        }
    }
}
