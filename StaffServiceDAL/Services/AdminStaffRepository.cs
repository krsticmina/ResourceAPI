using Microsoft.EntityFrameworkCore;
using StaffServiceAPI.DbContexts;
using StaffServiceDAL.Entities;

namespace StaffServiceDAL.Services
{
    public class AdminStaffRepository : IAdminStaffRepository
    {
        private readonly StaffDatabaseContext context;

        public AdminStaffRepository(StaffDatabaseContext context)
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

        /// <summary>
        /// Asynchronous method for retrieving all employees from database
        /// <returns></returns>
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await context.Employees.ToListAsync();
            
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
