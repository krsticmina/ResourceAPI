using Microsoft.EntityFrameworkCore;
using StaffServiceAPI.DbContexts;
using StaffServiceDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffServiceDAL.Services
{
    public class EmployeeStaffRepository : IEmployeeStaffRepository
    {
        private readonly StaffDatabaseContext context;

        public EmployeeStaffRepository(StaffDatabaseContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        ///  Asynchronous method for retrieving an employee using Id.
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {

            return await context.Employees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();

        }
    }
}
