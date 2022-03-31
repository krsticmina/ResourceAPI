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

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await context.Employees.Where(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await context.Employees.AddAsync(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            context.Employees.Update(employee);

        }
        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() >= 0;
        }
    }
}
