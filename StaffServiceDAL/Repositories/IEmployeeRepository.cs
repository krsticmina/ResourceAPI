using StaffServiceDAL.Entities;

namespace StaffServiceDAL.Services
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(Employee employee);
        Task<bool> SaveChangesAsync();

    }
}
