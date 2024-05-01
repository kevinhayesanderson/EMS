using Shared;

namespace Repository
{
    public interface IEmployeeRepository
    {
        Task<EmployeeDto> CreateEmployeeAsync(EmployeeDto_Admin employeeDto);

        Task DeleteEmployeeAsync(int id);

        Task<List<EmployeeDto>> GetAllEmployeesAsync();

        Task<EmployeeDto> GetEmployeeAsync(int id);

        Task UpdateEmployeeAsync(int id, EmployeeDto employeeDto);
    }
}