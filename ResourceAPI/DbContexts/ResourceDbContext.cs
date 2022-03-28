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
            
            base.OnModelCreating(modelBuilder);
        }
    }
}
