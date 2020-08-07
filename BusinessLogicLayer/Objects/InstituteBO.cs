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
            }
            catch (Exception ex)
            {
                throw;
            }
            
        }
    }
}
