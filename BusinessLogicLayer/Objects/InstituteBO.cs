using LastStudy.Core.DTO;
using LastStudy.Core.Entities;
using LastStudy.Core.Flags;
using LastStudy.Core.Helpers;
using LastStudy.Core.Interfaces.BOObjects;
using LastStudy.Core.Interfaces.DependencyInjector;
using LastStudy.Core.Interfaces.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Schema;

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
                int userInsSuccess = -1;
                int instituteCreationSuccess = -1;
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    if (!unitOfWork.InstituteConnections.FindByINSCode(instituteDTO.InstituteCode))
                    {
                        InstituteConnection insconnection = new InstituteConnection() { DatabaseName = instituteDTO.InstituteCode, InstituteCode = instituteDTO.InstituteCode, IsActive = true };
                        unitOfWork.InstituteConnections.Add(insconnection);
                        int insConnectionSuccess = unitOfWork.SaveLS();

                        if (insConnectionSuccess > 0)
                        {
                            UserInstitute userInstitute = new UserInstitute() { LSUserId = instituteDTO.UserId, InstituteConnectionId = insconnection.Id };
                            unitOfWork.UserInstitutes.Add(userInstitute);
                            userInsSuccess = unitOfWork.SaveLS();
                        }

                        if (userInsSuccess > 0)
                        {
                            unitOfWork.InitINS(this._insConnection);

                            Institute institute = new Institute()
                            {
                                Name = instituteDTO.Name,
                                InstituteType = instituteDTO.InstituteType,
                                ContactNumber = instituteDTO.ContactNumber,
                                Email = instituteDTO.Email,
                                PanNumber = instituteDTO.PanNumber,
                                InstituteCode = instituteDTO.InstituteCode
                            };
                            unitOfWork.Insitutes.Add(institute);
                            unitOfWork.SaveINS();

                            InstituteLocation instituteLocation = new InstituteLocation()
                            {
                                GstNumber = instituteDTO.GstNumber,
                                BranchCode = instituteDTO.BranchCode,
                                AddressLine1 = instituteDTO.AddressLine1,
                                AddressLine2 = instituteDTO.AddressLine2,
                                City = instituteDTO.City,
                                State = instituteDTO.State,
                                Country = instituteDTO.Country,
                                PinCode = instituteDTO.PinCode,
                                InstituteId = institute.Id,
                                PrimaryBranch = true
                            };
                            unitOfWork.InstituteLocations.Add(instituteLocation);
                            instituteCreationSuccess = unitOfWork.SaveINS();
                        }
                        if (instituteCreationSuccess > 0)
                        {
                            unitOfWork.InstituteUsers.Add(new InstituteUser() { Id = instituteDTO.UserId });
                            return unitOfWork.SaveINS();
                        }
                        else
                        {
                            return -1;
                        }
                        
                    }
                    return -1;
                }
               
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int AddUserInInstitute(int userId)
        {
            using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
            {
                unitOfWork.InitINS(this._insConnection);
                //if (unitOfWork.LSUsers.GetById(userId) != null)
                //{
                unitOfWork.InstituteUsers.Add(new InstituteUser() { Id = userId });
                return unitOfWork.SaveINS();
                //}
                //else
                //{
                //    return -1;
                //}
            }
        }

        public int AddUser(string email, int userID, string guid)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);

                    var userInvite = unitOfWork.InvitedUsers.GetByEmailGuid(email, guid);

                    if (userInvite != null)
                    {
                        if (userID >= 0)
                        {
                            var instituteConnection = unitOfWork.InstituteConnections.GetByINSCode(unitOfWork.InsDatabaseName());
                            int lsresult = 0;
                            if (instituteConnection != null)
                            {
                                unitOfWork.UserInstitutes.Add(new UserInstitute() { LSUserId = userID, InstituteConnectionId = instituteConnection.Id });
                                lsresult = unitOfWork.SaveLS();
                            }

                            if (lsresult > 0)
                            {
                                unitOfWork.InstituteUsers.Add(new InstituteUser() { UserId = userID });
                                unitOfWork.InstituteUserRoles.Add(new InstituteUserRole() { InstituteRoleId = userInvite.InstituteRoleId, InstituteUserId = userID });
                                return unitOfWork.SaveINS();
                            }
                        }
                    }
                }
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                        institute.Name = instituteDTO.Name;
                        institute.InstituteType = instituteDTO.InstituteType;
                        institute.ContactNumber = instituteDTO.ContactNumber;
                        institute.Email = instituteDTO.Email;
                        institute.PanNumber = instituteDTO.PanNumber;

                    }

                    var instituteLocation = unitOfWork.InstituteLocations.GetById(instituteDTO.InstituteLocationId);
                    if (instituteLocation != null)
                    {
                        instituteLocation.GstNumber = instituteDTO.GstNumber;
                        instituteLocation.BranchCode = instituteDTO.BranchCode;
                        instituteLocation.AddressLine1 = instituteDTO.AddressLine1;
                        instituteLocation.AddressLine2 = instituteDTO.AddressLine2;
                        instituteLocation.City = instituteDTO.City;
                        instituteLocation.State = instituteDTO.State;
                        instituteLocation.Country = instituteDTO.Country;
                        instituteLocation.PinCode = instituteDTO.PinCode;
                    }
                    return unitOfWork.SaveINS();
                }
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
                        CourseAmount = courseDTO.CourseAmount,
                        CourseCode = courseDTO.CourseCode,
                        Credit = courseDTO.Credit,
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

        public List<InstituteDTO> GetAllInstitutes(Dictionary<string, string> connectionInfo)
        {
            List<InstituteDTO> institutes = new List<InstituteDTO>();
            using (var unitofWork = _serviceLocator.Resolve<IUnitOfWork>())
            {
                var instituteConnections = unitofWork.InstituteConnections.GetActiveInstitutes().ToList();

                foreach (var instituteConnection in instituteConnections)
                {
                    connectionInfo.Remove("database");
                    connectionInfo.Add("database", instituteConnection.DatabaseName);
                    this._insConnection = connectionInfo.GenerateConnectionString();

                    unitofWork.InitINS(this._insConnection);
                    institutes.AddRange(unitofWork.Insitutes.GetAll().Select(x => new InstituteDTO
                    {
                        Name = x.Name,
                        InstituteType = x.InstituteType,
                        ContactNumber = x.ContactNumber,
                        Email = x.Email,
                        PanNumber = x.PanNumber,
                    }).ToList());
                }
            }
            return institutes;
        }

        public bool RemoveInstitute(string instituteCode)
        {
            using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
            {
                unitOfWork.InstituteConnections.GetByINSCode(instituteCode).IsActive = false;
                unitOfWork.SaveLS();
                return true;
            }
        }

        public List<SubjectDTO> GetAllSubjects(int courseID)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);

                    return unitOfWork.Subjects.GetSubjects(courseID).ToList().Select(x => new SubjectDTO { Name = x.Name, Id = x.Id }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CourseDTO> GetAllCourses(string instituteCode)
        {
            //try
            //{
            //    using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
            //    {
            //        unitOfWork.InitINS(this._insConnection);

            //        return unitOfWork.Courses.GetCourses(instituteCode).ToList().Select(x => new SubjectDTO { Name = x.Name, Id = x.Id }).ToList();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
            return null;
        }

        public int AddRole(RoleDTO roleDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    InstituteRole instituteRole = new InstituteRole()
                    {
                        Name = roleDTO.Name,
                        Description = roleDTO.Description
                    };
                    unitOfWork.InstituteRoles.Add(instituteRole);
                    return unitOfWork.SaveINS();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int UpdateRole(RoleDTO roleDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    InstituteRole instituteRole = unitOfWork.InstituteRoles.GetById(roleDTO.Id);
                    if (instituteRole != null)
                    {
                        instituteRole.Name = roleDTO.Name;
                        instituteRole.Description = roleDTO.Description;
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

        public bool SendUserInvite(UserInviteDTO userInviteDTO)
        {
            try
            {
                bool output = false;
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);

                    foreach (var email in userInviteDTO.Emails)
                    {
                        string guid = Guid.NewGuid().ToString();
                        byte[] guidBytes = new UnicodeEncoding().GetBytes(guid);

                        SHA256 sHash = SHA256.Create();

                        byte[] hashValue = sHash.ComputeHash(guidBytes);

                        unitOfWork.InvitedUsers.Add(new InvitedUser()
                        {
                            Email = email,
                            InstituteRoleId = userInviteDTO.RoleId,
                            CourseId = userInviteDTO.CourseId,
                            IsRegistered = false
                        });

                        int result = unitOfWork.SaveINS();

                        if (result > 0)
                        {
                            unitOfWork.LSInvitedUsers.Add(new LSInvitedUser()
                            {
                                InstituteCode = userInviteDTO.InstituteCode
                            });

                            if (unitOfWork.SaveLS() > 0)
                            {
                                output = SendEmail(email, userInviteDTO.Role, userInviteDTO.Course, guid);
                            }
                        }
                    }
                    return output;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool SendEmail(string email, string role, string course, string guid)
        {
            try
            {
                var fromAddress = new MailAddress("koibhiusekaro@gmail.com", "Last Study");
                var toAddress = new MailAddress(email, "To Name");
                const string fromPassword = "microsoftnokia";
                const string subject = "Invite to the institute";
                string url = "https://localhost:44365/api/v1/accounts/signup?invitecode=" + guid;
                string body = "You have been invited as the role :" + role + " for the course " + course + ". Please follow this link to join : " + url;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                    Timeout = 20000
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveUserInvite(UserInviteDTO userInviteDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    //send an email
                    foreach (var email in userInviteDTO.Emails)
                    {
                        var unregisteredUser = unitOfWork.InvitedUsers.GetUnregisteredByEmail(email);
                        unitOfWork.InvitedUsers.RemoveById(unregisteredUser.Id);
                    }
                    int result = unitOfWork.SaveINS();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool RemoveRole(int roleID)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    unitOfWork.InstituteRoles.RemoveById(roleID);
                    if (unitOfWork.SaveINS() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddInstituteLocation(InstituteLocationDTO instituteDTO)
        {

            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    if (!unitOfWork.InstituteConnections.FindByINSCode(instituteDTO.InstituteCode))
                    {
                        unitOfWork.InitINS(this._insConnection);

                        InstituteLocation instituteLocation = new InstituteLocation()
                        {
                            GstNumber = instituteDTO.GstNumber,
                            BranchCode = instituteDTO.BranchCode,
                            AddressLine1 = instituteDTO.AddressLine1,
                            AddressLine2 = instituteDTO.AddressLine2,
                            City = instituteDTO.City,
                            State = instituteDTO.State,
                            Country = instituteDTO.Country,
                            PinCode = instituteDTO.PinCode,
                            InstituteId = instituteDTO.InstituteId,
                            PrimaryBranch = true
                        };
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

        public bool RemoveInstituteLocation(int instittuteLocataionId)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    unitOfWork.InstituteLocations.RemoveById(instittuteLocataionId);
                    if (unitOfWork.SaveINS() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AddCourseGroup(CourseGroupDTO courseGroupDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    CourseGroup coursegroup = new CourseGroup()
                    {
                        CourseGroupCode = courseGroupDTO.CourseGroupCode,
                        Description = courseGroupDTO.Description,
                        Name = courseGroupDTO.Name
                    };
                    unitOfWork.CourseGroups.Add(coursegroup);
                    return unitOfWork.SaveINS();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditCourseGroup(CourseGroupDTO courseGroupDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    CourseGroup coursegroup = unitOfWork.CourseGroups.GetById(courseGroupDTO.Id);

                    if (coursegroup != null)
                    {
                        coursegroup.CourseGroupCode = courseGroupDTO.CourseGroupCode;
                        coursegroup.Description = courseGroupDTO.Description;
                        coursegroup.Name = courseGroupDTO.Name;
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
        public int EditCourse(CourseDTO courseDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    Course course = unitOfWork.Courses.GetById(courseDTO.Id);

                    if (course != null)
                    {
                        course.Name = courseDTO.Name;
                        course.CourseAmount = courseDTO.CourseAmount;
                        course.Credit = courseDTO.Credit;
                        course.CourseCode = courseDTO.CourseCode;
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

        public bool RemoveCourseGroup(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    unitOfWork.CourseGroups.RemoveById(id);
                    if (unitOfWork.SaveINS() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool RemoveCourse(int id)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    unitOfWork.Courses.RemoveById(id);
                    if (unitOfWork.SaveINS() > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CourseGroupDTO> ListCourseGroup()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    var courseGroups = unitOfWork.CourseGroups.GetAll().ToList().Select(x => new CourseGroupDTO { Id = x.Id, CourseGroupCode = x.CourseGroupCode, Description = x.Description, Name = x.Name }).ToList();
                    if (courseGroups.Count > 0)
                    {
                        return courseGroups;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RoleDTO> ListRoles()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    var roles = unitOfWork.InstituteRoles.GetAll().ToList().Select(x => new RoleDTO { Id = x.Id, Description = x.Description, Name = x.Name }).ToList();
                    if (roles.Count > 0)
                    {
                        return roles;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int EditInstituteLocation(InstituteLocationDTO instituteLocationDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    var instituteLocation = unitOfWork.InstituteLocations.GetById(instituteLocationDTO.Id);
                    if (instituteLocation != null)
                    {
                        instituteLocation.GstNumber = instituteLocationDTO.GstNumber;
                        instituteLocation.BranchCode = instituteLocationDTO.BranchCode;
                        instituteLocation.AddressLine1 = instituteLocationDTO.AddressLine1;
                        instituteLocation.AddressLine2 = instituteLocationDTO.AddressLine2;
                        instituteLocation.City = instituteLocationDTO.City;
                        instituteLocation.State = instituteLocationDTO.State;
                        instituteLocation.Country = instituteLocationDTO.Country;
                        instituteLocation.PinCode = instituteLocationDTO.PinCode;
                        instituteLocation.InstituteId = instituteLocationDTO.InstituteId;
                        instituteLocation.PrimaryBranch = true;

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

        public List<InstituteLocationDTO> GetAllInstituteLocations(int instituteID)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    var institituteLocations = unitOfWork.InstituteLocations.GetAllByInstitute(instituteID)
                        .Select(x => new InstituteLocationDTO()
                        {
                            Id = x.Id,
                            AddressLine1 = x.AddressLine1,
                            AddressLine2 = x.AddressLine2,
                            GstNumber = x.GstNumber,
                            BranchCode = x.BranchCode,
                            City = x.City,
                            Country = x.Country,
                            PinCode = x.PinCode,
                            State = x.State,
                            InstituteId = x.InstituteId
                        }).ToList();
                    return institituteLocations;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserInviteDTO> GetAllUserInvites()
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    var userInviteList = (from invite in unitOfWork.InvitedUsers.GetAll()
                                          group invite by new { invite.CourseId, CourseName = invite.Course.Name, invite.InstituteRoleId, RoleName = invite.InstituteRole.Name, invite.IsRegistered, invite.Message } into groupinvite
                                          select new UserInviteDTO
                                          {
                                              CourseId = groupinvite.Key.CourseId,
                                              Course = groupinvite.Key.CourseName,
                                              Role = groupinvite.Key.RoleName,
                                              RoleId = groupinvite.Key.InstituteRoleId,
                                              Message = groupinvite.Key.Message,
                                              Emails = groupinvite.Select(x => x.Email).ToList()
                                          }).ToList();

                    return userInviteList;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int AddCourseGroupCourse(CourseGroupCourseDTO courseGroupDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    unitOfWork.InitINS(this._insConnection);
                    CourseGroupCourse coursegroupcourse = new CourseGroupCourse()
                    {
                        CourseId = courseGroupDTO.CourseId,
                        CourseGroupId = courseGroupDTO.CourseGroupId
                    };
                    unitOfWork.CourseGroupCourses.Add(coursegroupcourse);
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

