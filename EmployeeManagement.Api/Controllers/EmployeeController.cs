using EmployeeManagement.Business;
using EmployeeManagement.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this._employeeService = employeeService;
        }


        [HttpGet]
        [Route("getAll")]
        public ActionResult GetAll()
        {
            var response = _employeeService.GetAll();
            return Ok(response);
        }

        [HttpGet]
        [Route("getAll/{id}")]
        public ActionResult GetAll(int id)
        {
            var response = _employeeService.GetAllFilterDepartment(id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _employeeService.DeleteEmployee(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EmployeeViewModel vm)
        {           
            var response = await _employeeService.SaveEmployee(vm);
            return Ok(response);
        }
    }
}
