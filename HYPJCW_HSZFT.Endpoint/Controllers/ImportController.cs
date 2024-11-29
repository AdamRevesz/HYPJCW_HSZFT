using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.AspNetCore.Mvc;

namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [Route("/api")]
    [ApiController]
    public class ImportController : ControllerBase

    {
        IImportLogic logic;

        public ImportController(IImportLogic logic)
        {
            this.logic = logic;
        }

        [HttpPost("/import/importjsurl")]
        public async Task ImportManagersFromJs(string url)
        {
            if(url is null)
            {
                throw new ArgumentException("The url is invalid");
            }
            this.logic.GetManagersJson(url);
        }
    }
}
