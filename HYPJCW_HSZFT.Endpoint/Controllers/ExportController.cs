using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        [HttpGet("export")]
        public IActionResult ExportEntities()
        {
            try
            {
                Type[] typesToExport = {
                    typeof(Employees),
                    typeof(Departments),
                    typeof(Managers)
                };

                XDocument xmlDocument = ExportLogic.ExportToXml(typesToExport);

                return Content(xmlDocument.ToString(), "application/xml");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
