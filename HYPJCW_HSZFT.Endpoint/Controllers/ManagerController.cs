using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.AspNetCore.Mvc;
namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerLogic managerLogic;

        public ManagerController(IManagerLogic managerLogic)
        {
            this.managerLogic = managerLogic;
        }

        [HttpPost]
        public IActionResult Create([FromBody] Managers manager)
        {
            try
            {
                managerLogic.Create(manager);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("/Delete{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                managerLogic.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Read{id}")]
        public ActionResult<Managers> Read(string id)
        {
            try
            {
                var manager = managerLogic.Read(id);
                return Ok(manager);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Managers>> ReadAll()
        {
            var managers = managerLogic.ReadAll();
            return Ok(managers);
        }

        [HttpPut("/Update{id}")]
        public IActionResult Update(string id, [FromBody] Managers manager)
        {
            try
            {
                managerLogic.Update(manager, id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("doctorate-without-mba")]
        public ActionResult<List<Managers>> GetAllManagersWithDoctorateWithouthMba()
        {
            try
            {
                var managers = managerLogic.GetAllManagersWithDoctorateWithouthMba();
                return Ok(managers);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("longest-working")]
        public ActionResult<Managers> GetLongestWorkingManager()
        {
            try
            {
                var manager = managerLogic.GetLongestWorkingManager();
                return Ok(manager);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("longest-working-age")]
        public ActionResult<Managers> GetLongestWorkingManagerComparedToHisAge()
        {
            try
            {
                var manager = managerLogic.GetLongestWorkingManagerComparedToHisAge();
                return Ok(manager);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("doctorate")]
        public ActionResult<List<Managers>> GetManagersWithDoctorate()
        {
            var managers = managerLogic.GetManagersWithDoctorate();
            return Ok(managers);
        }

        [HttpGet("mba-rate")]
        public IActionResult GetRateOfManagersWithMbaAndWithout()
        {
            managerLogic.GetRateOfManagersWithMbaAndWithout();
            return Ok();
        }
    }
}

