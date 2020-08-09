using LastStudy.Core.DTO;
using LastStudy.Core.Entities;
using LastStudy.Core.Interfaces.BOObjects;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Objects
{
    public class InstituteBO : IInstituteBO
    {
        private readonly IServiceLocator _serviceLocator;
        private string _insConnection;
        public InstituteBO(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

        public void InitINS(string connectionName)
        {
            this._insConnection = connectionName;
        }

        public int AddInstitute(InstituteDTO instituteDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    if (!unitOfWork.InsituteConnections.FindByINSCode(instituteDTO.InstituteCode))
                    {
                        InstituteConnection insconnection = new InstituteConnection() { DatabaseName = instituteDTO.InstituteCode, InstituteCode = instituteDTO.InstituteCode };
                        unitOfWork.InsituteConnections.Add(insconnection);
                        unitOfWork.SaveLS();

                        UserInstitute userInstitute = new UserInstitute() { LSUserId = instituteDTO.UserId, InstituteConnectionId = insconnection.Id };
                        unitOfWork.UserInstitutes.Add(userInstitute);

                        unitOfWork.InitINS(this._insConnection);
                        Institute institute = new Institute()
                        {
                            Name = instituteDTO.Name,
                            Email = instituteDTO.Email,
                            Address = instituteDTO.Address,
                            InstituteCode = instituteDTO.InstituteCode
                        };
                        unitOfWork.Insitutes.Add(institute);
                        unitOfWork.SaveLS();
                        return unitOfWork.SaveINS();
                    }
                    else
                    {
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public int AddUser(UserCreateDTO userCreateDTO, int userID)
        {
            //try
            //{
            //    using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
            //    {
            //        if (userCreateDTO.IsStudent)
            //        {
            //            unitOfWork.Students.Add(new Student() { })
            //        }
            //        else if (userCreateDTO.IsTeacher)
            //        {

            //        }
            //    }

            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return 0;
        }

        public int EditInstitute(InstituteDTO instituteDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    var institute = unitOfWork.Insitutes.FindById(instituteDTO.Id);

                    if (institute != null)
                    {
                        institute.Address = instituteDTO.Address;
                        institute.Email = instituteDTO.Email;
                        institute.PhoneNumber = instituteDTO.PhoneNumber;
                        institute.MobileNumber = instituteDTO.MobileNumber;
                        institute.City = instituteDTO.City;
                        institute.Country = instituteDTO.Country;
                        return unitOfWork.SaveINS();
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddCourse(CourseDTO courseDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    Course course = new Course()
                    {
                        InstituteId = courseDTO.InstituteId,
                        Name = courseDTO.Name
                    };
                    unitOfWork.Courses.Add(course);
                    return unitOfWork.SaveINS();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddSubject(SubjectDTO subjectDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    Subject subject = new Subject()
                    {
                        CourseId = subjectDTO.CourseId,
                        Name = subjectDTO.Name
                    };
                    unitOfWork.Subjects.Add(subject);
                    return unitOfWork.SaveINS();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
