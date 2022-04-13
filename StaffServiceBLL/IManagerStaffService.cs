using StaffServiceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL
{
    public interface IManagerStaffService
    {
        Task<EmployeeDto?> AddEmployeeAsync(EmployeeForInsertionDto employeeToAdd);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int managerId, int employeeId);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync(int managerId);
        Task<EmployeeForUpdateDto?> GetEmployeeForUpdateAsync(int managerId, int employeeId);
        Task<EmployeeDto?> UpdateEmployeeAsync(int employeeId, int managerId, EmployeeForUpdateDto employeeToUpdate);
    }
}

