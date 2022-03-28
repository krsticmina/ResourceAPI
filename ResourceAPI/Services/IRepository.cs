using ResourceAPI.Entities;

namespace ResourceAPI.Services
{
    public interface IRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync();

        Task AddEmployeeAsync(Employee employee);
        Task<bool> SaveChangesAsync();
    }
}
