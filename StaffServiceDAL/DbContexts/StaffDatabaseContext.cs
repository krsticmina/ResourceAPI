using Microsoft.EntityFrameworkCore;
using StaffServiceDAL.Entities;

namespace StaffServiceAPI.DbContexts
{
    public class StaffDatabaseContext : DbContext
    {
        public StaffDatabaseContext(DbContextOptions options) : base(options)
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
                   Position = "Admin"
               },
                new Employee("Ema Vuckovic")
               {
                   Id = 2,
                   Email = "ev@gmail.com",
                   Position = "Manager"
               },
                new Employee("Adam Cimbaljevic") 
                {
                    Id = 3,
                    Email = "acim@gmail.com",
                    Position = "Employee",
                    ManagerId = 2

                }
               );
         
            base.OnModelCreating(modelBuilder);
        }
    }
}
