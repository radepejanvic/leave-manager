using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;
using static System.Environment;

namespace DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public void Initialize()
        {
            try
            {
                if (_db.Database.GetPendingMigrations().Count() > 0)
                {
                    _db.Database.Migrate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error during DbInitialization: {e}");
            }

            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
              
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();


                var email = GetEnvironmentVariable("ADMIN_EMAIL");
                var password = GetEnvironmentVariable("ADMIN_PASSWORD");

                var result = _userManager.CreateAsync(new IdentityUser
                {
                    Email = email,
                    UserName = email,
                }, password).GetAwaiter().GetResult();

                IdentityUser user = _db.Users.FirstOrDefault(u => u.Email == GetEnvironmentVariable("ADMIN_EMAIL"));
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
        }
    }
}
