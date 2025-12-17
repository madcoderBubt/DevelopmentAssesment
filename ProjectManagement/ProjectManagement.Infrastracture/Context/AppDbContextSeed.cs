using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using ProjectManagement.Core.Entities;
using ProjectManagement.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.Context
{
    public class AppDbContextSeed
    {
        public static async Task SeedAsync(AppDbContext dbContext, ILogger<AppDbContext> logger)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("Database Context is null.");
            }

            if (!dbContext.Users.Any())
            {
                dbContext.Users.AddRange(GetUserList());
                await dbContext.SaveChangesAsync();
            }
        }

        private static IList<UserEntity> GetUserList()
        {
            IList<UserEntity> users = new List<UserEntity>();
            users.Add(new()
            {
                FullName = "Administrator",
                Role = EUserRole.Admin,
                Email = "admin@demo.com",
                Password = "Admin123!",

            });
            users.Add(new()
            {
                FullName = "Manager",
                Role = EUserRole.Admin,
                Email = "manager@demo.com",
                Password = "Manager123!",

            });
            users.Add(new()
            {
                FullName = "Employee",
                Role = EUserRole.Admin,
                Email = "employee@demo.com",
                Password = "Employee123!",

            });

            return users;
        }
    }
}
