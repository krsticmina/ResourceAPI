using StaffServiceDAL.Entities;

namespace StaffServiceDAL.Services
{
    public interface IAdminStaffRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();
        Task AddEmployeeAsync(Employee employee);
        Task<bool> SaveChangesAsync();

    }
}
