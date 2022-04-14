using Microsoft.AspNetCore.JsonPatch;
using StaffServiceBLL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL
{
    public interface IEmployeeService
    {
        Task<EmployeeModel?> GetEmployeeByIdAsync(int employeeId);
        Task<IEnumerable<EmployeeModel>> GetAllEmployeesAsync();
        Task<EmployeeModel?> AddEmployeeAsync(EmployeeForInsertionModel employeeToAdd);
        Task<EmployeeModel?> UpdateEmployeeAsync(int employeeId, EmployeeForUpdateModel employeeToUpdate);
    
    }
}
