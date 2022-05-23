using Microsoft.EntityFrameworkCore;
using StaffServiceAPI.DbContexts;
using StaffServiceDAL.Entities;

namespace StaffServiceDAL.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly StaffDatabaseContext context;

        public EmployeeRepository(StaffDatabaseContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        ///<inheritdoc/>
        public async Task<Employee?> GetEmployeeByIdAsync(int employeeId)
        {

            return await context.Employees.Where(e => e.Id == employeeId).FirstOrDefaultAsync();

        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await context.Employees.ToListAsync();

        }

        ///<inheritdoc/>
        public async Task AddEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
        }


        ///<inheritdoc/>
        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() >= 0;
        }

        ///<inheritdoc/>
        public async Task<IEnumerable<Employee>> GetAllEmployeesForManagerAsync(int managerId)
        {
            return await context.Employees.FromSqlInterpolated($"WITH EmployeeCTE AS(SELECT * FROM [Employees] WHERE ManagerId = {managerId} UNION ALL SELECT emp.* FROM [Employees] AS emp INNER JOIN EmployeeCTE AS mgr ON emp.ManagerId = mgr.Id WHERE emp.ManagerId IS NOT NULL) SELECT * FROM EmployeeCTE").ToListAsync();
        }

        ///<inheritdoc/>
        public async Task<Employee?> CheckIfManagerIsAuthorizedAsync(int employeeId, int managerId)
        {
            var employees = await context.Employees.FromSqlInterpolated($"WITH EmployeeCTE AS(SELECT * FROM [Employees] WHERE ManagerId = {managerId} UNION ALL SELECT emp.* FROM [Employees] AS emp INNER JOIN EmployeeCTE AS mgr ON emp.ManagerId = mgr.Id WHERE emp.ManagerId IS NOT NULL) SELECT * FROM EmployeeCTE").ToListAsync();
            
            return employees.Where(e => e.Id == employeeId).FirstOrDefault();

        }

        ///<inheritdoc/>
        public Task<Employee?> FindUserInDatabaseAsync(int userId)
        {
            return context.Employees.Where(e => e.UserId == userId).FirstOrDefaultAsync();
        }

    }
}

