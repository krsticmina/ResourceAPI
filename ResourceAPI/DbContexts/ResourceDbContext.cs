using Microsoft.EntityFrameworkCore;
using ResourceAPI.Entities;

namespace ResourceAPI.DbContexts
{
    public class ResourceDbContext : DbContext
    {
        public ResourceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee("Jana Filipovic") 
                {
                    Id = 1,
                    Email = "jf@gmail.com",
                    PhoneNumber = "+3816754329"
                },
                new Employee("Ema Vuckovic")
                {
                    Id = 2,
                    Email = "ev@gmail.com",
                    PhoneNumber = "+38167235735"
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
