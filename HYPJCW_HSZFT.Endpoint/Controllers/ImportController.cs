using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Xml.Linq;

namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [Route("/api")]
    [ApiController]
    public class ImportController : ControllerBase

    {
        private readonly IImportLogic _logic;

        public ImportController(IImportLogic logic)
        {
            _logic = logic;
        }

        [HttpPost("/import/importjs")]
        public async Task ImportManagersFromJs(string url)
        {
            if(url is null)
            {
                throw new ArgumentException("The url is invalid");
            }
            await _logic.GetManagersJson(url);
        }

        [HttpPost("/import/importxml")]
        public async Task ImportEmployeesFromXML([FromBody] string url)
        {
            if (url is null)
            {
                throw new ArgumentException("The url is invalid");
            }
            await _logic.GetEmployeesXml(url);
        }
    }
}
