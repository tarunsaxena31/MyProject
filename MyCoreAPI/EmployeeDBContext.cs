using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyCoreAPI
{
    public class EmployeeDBContext : DbContext
    {

        //public EmployeeDBContext(string ConnectionString) : base(new DbContextOptionsBuilder().UseSqlServer(ConnectionString).Options)
        //{

        //}

        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    _ = modelBuilder.Query<Employee>();
        //}
        public static string ConnectionString { get; set; }
        //public EmployeeDBContext() { }
        
        //public EmployeeDBContext(string connectionString) : base(GetOptions(connectionString)) { }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!string.IsNullOrEmpty(ConnectionString)) optionsBuilder.UseSqlServer(ConnectionString);
        //}
    }
}
