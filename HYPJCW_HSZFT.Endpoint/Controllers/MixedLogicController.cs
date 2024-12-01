using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Entities.Entity_Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using HYPJCW_HSZFT.Models.Entity_Models;
using HYPJCW_HSZFT.Models.DTOs;

namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MixedLogicController : ControllerBase
    {
        private readonly IMixedLogic mixedLogic;

        public MixedLogicController(IMixedLogic mixedLogic)
        {
            this.mixedLogic = mixedLogic;
        }

        [HttpGet("who-works-for-the-longest")]
        public ManagerOrEmployeeDto WhoWorksForTheLongest()
        {
            return mixedLogic.WhoWorksForTheLongest();
        }

        [HttpGet("is-there-manager-who-is-department-manager")]
        public List<Managers> IsThereManagerWhoIsDepartmentManager()
        {
            var result = mixedLogic.IsThereManagerWhoIsDepartmentManager();
            return result;
        }

        [HttpGet("who-are-managers-or-department-managers")]
        public List<DepartmentOrManagersDto> WhoAreManagersOrDepartmentManagers()
        {
            var result = mixedLogic.WhoAreManagersOrDepartmentManagers();
            return result;
        }
    }
}
