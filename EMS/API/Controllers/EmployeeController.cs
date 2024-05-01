using API.ActionFilters;
using Microsoft.AspNetCore.Mvc;
using Repository;
using Shared;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController(IEmployeeRepository _repository) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetEmployees()
        {
            var employees = await _repository.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetEmployee(int id)
        {
            var employee = await _repository.GetEmployeeAsync(id);
            return Ok(employee);
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidateAdminRoleType))]
        public async Task<IActionResult> CreateEmployee([FromBody] EmployeeDto_Admin employeeDto)
        {
            var employee = await _repository.CreateEmployeeAsync(employeeDto);
            return Ok(employee);
        }

        [HttpDelete("{id:int}")]
        [ServiceFilter(typeof(ValidateAdminRoleType))]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            await _repository.DeleteEmployeeAsync(id);
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ServiceFilter(typeof(ValidateAdminRoleType))]
        public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeDto employeeDto)
        {
            await _repository.UpdateEmployeeAsync(id, employeeDto);
            return NoContent();
        }
    }
}