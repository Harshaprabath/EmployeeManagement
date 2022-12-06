using EmployeeManagement.Data.Configurations;
using EmployeeManagement.Model;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Data.Data
{
    public class EmployeeManagementContext : DbContext
    {
        public EmployeeManagementContext()
        {

        }
        public EmployeeManagementContext(DbContextOptions<EmployeeManagementContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-5IGVKJE\\SQLEXPRESS;Database=EmployeeManagement;User Id=sa;Password=1qaz2wsx@");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Database Entities Configurations
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());


        }

        //Database Entities
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Project> Project { get; set; }
    }
}
