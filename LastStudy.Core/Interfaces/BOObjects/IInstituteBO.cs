using LastStudy.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LastStudy.Core.Interfaces.BOObjects
{
    public interface IInstituteBO
    {
        int AddInstitute(InstituteDTO instituteDTO);
        int EditInstitute(InstituteDTO instituteDTO);
        int AddCourse(CourseDTO courseDTO);
        int AddSubject(SubjectDTO subjectDTO);
        void InitINS(string connectionName);
        
        int AddUser(UserCreateDTO userCreateDTO, int userID);
    }
}
