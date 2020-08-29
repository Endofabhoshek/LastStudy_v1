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
        int AddInstituteLocation(InstituteLocationDTO instituteDTO);
        int EditInstitute(InstituteDTO instituteDTO);
        int AddCourse(CourseDTO courseDTO);
        int AddSubject(SubjectDTO subjectDTO);
        int AddRole(RoleDTO roleDTO);
        int UpdateRole(RoleDTO roleDTO);
        bool RemoveRole(int roleID);
        void InitINS(string connectionName);
        bool SendUserInvite(UserInviteDTO userInviteDTO);

        int AddUser(string email, int userID, string guid);
        List<InstituteDTO> GetAllInstitutes(Dictionary<string, string> connectionInfo);
        bool RemoveInstitute(string instituteCode);
        bool RemoveInstituteLocation(int instittuteLocataionId);
        List<SubjectDTO> GetAllSubjects(int courseID);
        List<CourseDTO> GetAllCourses(string instituteCode);
        bool RemoveUserInvite(UserInviteDTO userInviteDTO);
        int AddCourseGroup(CourseGroupDTO courseGroupDTO);
        int AddCourseGroupCourse(CourseGroupCourseDTO courseGroupDTO);
        int EditCourseGroup(CourseGroupDTO courseGroupDTO);
        int EditCourse(CourseDTO courseDTO);
        bool RemoveCourseGroup(int id);
        bool RemoveCourse(int id);
        List<CourseGroupDTO> ListCourseGroup();
        List<RoleDTO> ListRoles();
        int EditInstituteLocation(InstituteLocationDTO instituteLocationDTO);
        List<InstituteLocationDTO> GetAllInstituteLocations(int instituteID);
        List<UserInviteDTO> GetAllUserInvites();
    }
}
