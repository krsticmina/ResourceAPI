using Microsoft.EntityFrameworkCore;
using ResourceAPI.Entities;

namespace ResourceAPI.DbContexts
{
    public class ResourceDbContext : DbContext
    {
        public ResourceDbContext(DbContextOptions options) : base(options)
        {
        }

        DbSet<Employee> Employees { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData
                (
                    new Employee("Mina Krstic") 
                    {
                        Id=1,
                        Email="minakrsticmail@gmail.com",
                        PhoneNumber="+38169612286"
                    },
                    new Employee("Jana Filipovic")
                    {
                        Id = 2,
                        Email = "janafilipovicmail@gmail.com",
                        PhoneNumber = "+381627733827"
                    }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
