namespace Shared
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Role { get; set; }
    }

    public class EmployeeDto_Admin : EmployeeDto
    {
        public decimal Salary { get; set; }
    }
}