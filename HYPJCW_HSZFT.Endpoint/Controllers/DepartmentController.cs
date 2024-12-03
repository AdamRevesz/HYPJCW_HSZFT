﻿using HYPJCW_HSZFT.Entities.Entity_Models;
using HYPJCW_HSZFT.Logic;
using HYPJCW_HSZFT.Logic.Interfaces;
using HYPJCW_HSZFT.Models.Entity_Models;
using Microsoft.AspNetCore.Mvc;

namespace HYPJCW_HSZFT.Endpoint.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentLogic departmentLogic;

        public DepartmentController(IDepartmentLogic departmentLogic)
        {
            this.departmentLogic = departmentLogic;
        }

        [HttpPost("/Department")]
        public IActionResult Create([FromBody] Departments department)
        {
            try
            {
                departmentLogic.Create(department);
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
                departmentLogic.Delete(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("/Read{id}")]
        public ActionResult<Departments> Read(string id)
        {
            try
            {
                var manager = departmentLogic.Read(id);
                return Ok(manager);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public ActionResult<List<Departments>> ReadAll()
        {
            var managers = departmentLogic.ReadAll();
            return Ok(managers);
        }

        [HttpPut("/Update{id}")]
        public IActionResult Update(string id, [FromBody] Departments manager)
        {
            try
            {
                departmentLogic.Update(manager, id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}