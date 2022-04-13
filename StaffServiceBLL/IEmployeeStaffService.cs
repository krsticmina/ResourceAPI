using StaffServiceCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceBLL
{
    public interface IEmployeeStaffService
    {
        Task<EmployeeDto?> GetEmployeeByIdAsync( int employeeId);
    }
}
