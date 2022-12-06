using EmployeeManagement.Business;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.ViewModel;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;

        public EmployeeController(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        [Route("getAll")]
        public ActionResult GetAll()
        {
            var response = employeeService.GetAll();
            return Ok(response);
        }

        [HttpGet]
        [Route("getAll/{id}")]
        public ActionResult GetAll(int id)
        {
            var response = employeeService.GetAllFilterDepartment(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await employeeService.DeleteEmploye(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeViewModel vm)
        {           
            var response = await employeeService.SaveSubject(vm);
            return Ok(response);
        }
    }
}
