using Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Context : DbContext
    {
        public DbSet<Employee> Employees { get; set; }

        public Context()
        {
        }

        public Context(DbContextOptions<Context> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasData(
                new Employee { Id = 1, Name = "name1", Email = "email1", Position = "position1", Role = "Admin", Salary = 50000 },
                new Employee { Id = 2, Name = "name2", Email = "email2", Position = "position2", Role = "Employee", Salary = 30000 },
                new Employee { Id = 3, Name = "name3", Email = "email3", Position = "position3", Role = "Employee", Salary = 30000 });
        }
    }
}