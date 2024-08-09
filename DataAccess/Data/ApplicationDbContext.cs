using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;
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
        public DbSet<LeaveRequest> LeaveRequests { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Employee>().HasData(
                new Employee
                {
                    Email = "marko@example.com",
                    Name = "Marko",
                    Surname = "Markovic",
                    Phone = "0631239999"
                },
                new Employee
                {
                    Email = "milan@example.com",
                    Name = "Milan",
                    Surname = "Mladenovic",
                    Phone = "0631234999"
                },
                new Employee
                {
                    Email = "ajs@nigucci.com",
                    Name = "Vladan",
                    Surname = "Aksentijevic",
                    Phone = "0631233999"
                },
                new Employee
                {
                    Email = "radefaks@gmail.com",
                    Name = "Rade",
                    Surname = "Pejanovic",
                    Phone = "0631235999"
                }
            );

            builder.Entity<LeaveRequest>().HasData(
                new LeaveRequest
                {
                    Id = 1,
                    EmployeeEmail = "ajs@nigucci.com",
                    Start = DateOnly.FromDateTime(DateTime.Now),
                    End = DateOnly.FromDateTime(DateTime.Now.AddDays(5)),
                    Duration = 5
                },
                new LeaveRequest
                {
                    Id = 2,
                    EmployeeEmail = "ajs@nigucci.com",
                    Start = DateOnly.FromDateTime(DateTime.Now.AddMonths(4)),
                    End = DateOnly.FromDateTime(DateTime.Now.AddMonths(5)),
                    Duration = 30
                },
                new LeaveRequest
                {
                    Id = 3,
                    EmployeeEmail = "milan@example.com",
                    Start = DateOnly.FromDateTime(DateTime.Now),
                    End = DateOnly.FromDateTime(DateTime.Now.AddDays(5)), 
                    Duration = 5
                });
        }

    }
}
