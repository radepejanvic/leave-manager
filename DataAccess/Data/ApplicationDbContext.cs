using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Email = "marko@example.com",
                    Name = "Marko",
                    Surname = "Markovic",
                    Phone = "0631239999"
                },
                new Employee
                {
                    Id = 2,
                    Email = "milan@example.com",
                    Name = "Milan",
                    Surname = "Mladenovic",
                    Phone = "0631234999"
                },
                new Employee
                {
                    Id = 3,
                    Email = "ajs@nigucci.com",
                    Name = "Vladan",
                    Surname = "Aksentijevic",
                    Phone = "0631233999"
                }
                );
        }

    }
}
