using StaffServiceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceDAL.Services
{
    public interface IManagerStaffRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
        Task<Employee?> GetEmployeeByIdAsync(int managerId, int employeeId);
        Task<IEnumerable<Employee>> GetAllEmployeesAsync(int managerId);
        Task AddEmployeeAsync(Employee employee);
        Task<bool> SaveChangesAsync();
    }
}
