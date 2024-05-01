using Entities;
using Entities.Exceptions;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Repository
{
    public class EmployeeRepository(Context _context) : IEmployeeRepository
    {
        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _context.Employees.ToListAsync();
            return employees
                .Select(e => new EmployeeDto { Id = e.Id, Name = e.Name, Email = e.Email, Position = e.Position, Role = e.Role })//should use auto mapper
                .ToList();
        }

        public async Task<EmployeeDto> GetEmployeeAsync(int id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == id);
            return employee == null ? throw new EmployeeNotFoundException(id) :
                new EmployeeDto { Id = employee.Id, Name = employee.Name, Email = employee.Email, Position = employee.Position, Role = employee.Role };
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto_Admin employeeDto)
        {
            _context.Employees.Add(new Employee { Name = employeeDto.Name, Email = employeeDto.Email, Position = employeeDto.Position, Role = employeeDto.Role, Salary = employeeDto.Salary });
            await _context.SaveChangesAsync();
            return employeeDto;
        }

        public async Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == id) ?? throw new EmployeeNotFoundException(id);
            employee.Id = id;
            employee.Name = employeeDto.Name;
            employee.Email = employeeDto.Email;
            employee.Position = employeeDto.Position;
            employee.Role = employeeDto.Role;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var employee = await _context.Employees.SingleOrDefaultAsync(x => x.Id == id) ?? throw new EmployeeNotFoundException(id);
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
        }
    }
}