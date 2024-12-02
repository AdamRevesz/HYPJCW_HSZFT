using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly IExportLogic _logic;

        public ExportController(IExportLogic logic)
        {
            _logic = logic;
        }

        [HttpGet("/export/exportclassdata")]
        public void ExportEntities()
        {
            Type[] typesToExport =
            {
              typeof(Employees),
              typeof(Departments),
              typeof(Managers)
            };
            _logic.ExportToXml(typesToExport);
        }
    }
}
