using StaffServiceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceDAL.Services
{
    public interface IEmployeeStaffRepository
    {
        Task<Employee?> GetEmployeeByIdAsync(int employeeId);
    }
}
