namespace Entities.Exceptions
{
    public class EmployeeNotFoundException(int id) : Exception($"Employee with id: {id} doesn't exist in the database.")
    {
    }
}