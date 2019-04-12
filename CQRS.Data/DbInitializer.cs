using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CQRS.Data
{
    public class DbInitializer
    {
        UserManager<IdentityUser> _userManager;
        RoleManager<IdentityRole> _roleManager;
        IdentityDbContext<IdentityUser> _context;

        public DbInitializer(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IdentityDbContext<IdentityUser> context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            _context.Database.EnsureCreated();

            if (!_context.Users.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    IdentityUser user = new IdentityUser { UserName = "User" + i };

                    var r = _userManager.CreateAsync(user, "123!@#qweQWE")
                        .ConfigureAwait(false).GetAwaiter().GetResult();

                    if (!r.Succeeded)
                    {
                        var err = r.Errors.First();
                        throw new Exception(string.Format("code {0} descr {1}", err.Code, err.Description));
                    }
                }
            }
        }
    }
}
