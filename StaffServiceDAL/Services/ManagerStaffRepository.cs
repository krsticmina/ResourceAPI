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
    public class ManagerStaffRepository : IManagerStaffRepository
    {
        private readonly StaffDatabaseContext context;

        public ManagerStaffRepository(StaffDatabaseContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Asynchronous method for getting an employee from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee?> GetEmployeeByIdAsync(int managerId, int employeeId)
        {

            return await context.Employees.Where(e => e.Id == employeeId && e.ManagerId == managerId).FirstOrDefaultAsync();

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


        /// <summary>
        /// Asynchronous method for retrieving all employees from database
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync(int managerId)
        {
            return await context.Employees.Where(e => e.ManagerId == managerId).ToListAsync();
            
        }


        /// <summary>
        /// Asynchronous method for adding an employee to database
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task AddEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
        }


        /// <summary>
        /// Asynchronous method for saving changes made to database
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() >= 0;
        }
    }
}
