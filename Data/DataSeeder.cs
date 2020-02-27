using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Data
{
    public class DataSeeder
    {
        private ApplicationDbContext _context;

        public DataSeeder(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task SeedSuperUser()
        {
            RoleStore<IdentityRole> roleStore
            = new RoleStore<IdentityRole>(_context);

            UserStore<ApplicationUser> userStore
            = new UserStore<ApplicationUser>(_context);

            ApplicationUser user = new ApplicationUser
            {
                UserName = "ForumAdmin",
                NormalizedUserName = "forumadmin",
                Email = "admin@example.com",
                NormalizedEmail = "admin@example.com",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString()
            };

            user.PasswordHash = new PasswordHasher<ApplicationUser>()
            .HashPassword(user, "admin");

            bool hasAdminRole = _context.Roles
            .Any(roles => roles.Name == "Admin");

            if (!hasAdminRole)
            {
                roleStore.CreateAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "admin"
                });
            }

            bool hasSuperUser = _context.Users
            .Any(u => u.NormalizedUserName == user.UserName);

            if (!hasSuperUser)
            {
                userStore.CreateAsync(user);
                userStore.AddToRoleAsync(user, "Admin");
            }

            _context.SaveChangesAsync();

            return Task.CompletedTask;
        }
    }
}