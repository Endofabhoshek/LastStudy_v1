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
        public InstituteBO(IServiceLocator serviceLocator)
        {
            this._serviceLocator = serviceLocator;
        }

        public int AddInstitute(InstituteDTO instituteDTO)
        {
            try
            {
                using (var unitOfWork = _serviceLocator.Resolve<IUnitOfWork>())
                {
                    InstituteConnection insconnection = new InstituteConnection() { DatabaseName = instituteDTO.InstituteCode, InstituteCode = instituteDTO.InstituteCode };
                    unitOfWork.InsituteConnections.Add(insconnection);
                    unitOfWork.Save();

                    UserInstitute userInstitute = new UserInstitute() { LSUserId = instituteDTO.UserId, InstituteConnectionId = insconnection.Id };
                    unitOfWork.UserInstitutes.Add(userInstitute);

                    return unitOfWork.Save();
                    //Institute institute = new Institute()
                    //{
                    //    Name = instituteDTO.Name,
                    //    Email = instituteDTO.Email,
                    //    Address = instituteDTO.Address,
                    //    InstituteCode = instituteDTO.InstituteCode
                    //};
                    
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
