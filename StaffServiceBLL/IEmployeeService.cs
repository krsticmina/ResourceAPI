using Microsoft.AspNetCore.JsonPatch;
using StaffServiceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL
{
    public interface IEmployeeService
    {
        Task<EmployeeForUpdateDto?> GetEmployeeForUpdate(int employeeId);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> AddEmployeeAsync(EmployeeForInsertionDto employeeToAdd);
        Task<EmployeeDto?> UpdateEmployeeAsync(int employeeId, EmployeeForUpdateDto employeeToUpdate);
    }
}
