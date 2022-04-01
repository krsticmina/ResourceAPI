using Microsoft.EntityFrameworkCore;
using ResourceAPI.DbContexts;
using ResourceAPI.Entities;

namespace ResourceAPI.Services
{
    public class Repository : IRepository
    {
        private readonly ResourceDbContext context;

        public Repository(ResourceDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <summary>
        /// Asynchronous method for getting an employee from database by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
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
        /// Method for updating an employee
        /// </summary>
        /// <param name="employee"></param>
        public void UpdateEmployee(Employee employee)
        {
            context.Employees.Update(employee);

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
