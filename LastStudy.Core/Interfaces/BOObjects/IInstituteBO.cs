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
    }
}
