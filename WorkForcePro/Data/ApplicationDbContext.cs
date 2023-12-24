using WorkForcePro.Models;
using Microsoft.EntityFrameworkCore;

namespace WorkForcePro.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Company> Companies { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Shift> Shifts { get; set; }
        public DbSet<EmployeeAttendance> Attendence { get; set; }
        public DbSet<AttendanceSummary> AttendanceSummaries { get; set; }
        public DbSet<Salary> Salaries { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Salary>()
            .HasKey(a => new { a.ComId, a.EmpId, a.dtYear, a.dtMonth });

            modelBuilder.Entity<AttendanceSummary>()
            .HasKey(a => new { a.ComId, a.EmpId, a.DtYear, a.DtMonth });

            modelBuilder.Entity<EmployeeAttendance>()
            .HasKey(a => new { a.ComId, a.EmpId, a.dtDate });

            base.OnModelCreating(modelBuilder);
        }
    }
}
