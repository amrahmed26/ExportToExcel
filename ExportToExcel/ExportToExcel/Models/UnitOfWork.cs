using Microsoft.EntityFrameworkCore;

namespace ExportToExcel.Models
{
    public class UnitOfWork:DbContext
    {
        public UnitOfWork(DbContextOptions<UnitOfWork> options):base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().Property(x=>x.FirstName).HasMaxLength(20).IsRequired();
            modelBuilder.Entity<Employee>().Property(x=>x.LastName).HasMaxLength(20).IsRequired();
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
