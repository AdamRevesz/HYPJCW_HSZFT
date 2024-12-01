using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Models.DTOs;
using HYPJCW_HSZFT.Models.Entity_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HYPJCW_HSZFT.Logic.Interfaces
{
    public interface IMixedLogic
    {
        ManagerOrEmployeeDto WhoWorksForTheLongest();
        List<Managers> IsThereManagerWhoIsDepartmentManager();
        List<DepartmentOrManagersDto> WhoAreManagersOrDepartmentManagers();
    }
}
