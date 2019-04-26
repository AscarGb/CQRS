using CQRS.Common;
using CQRS.Data;
using CQRS.Domain.Queries.Query;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.Handler
{
    public class AllUsersQueryHandler : IQueryHandler<AllUsersQuery, Task<IEnumerable<User>>>
    {
        UserManager<IdentityUser> _userManager;
        public AllUsersQueryHandler(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IEnumerable<User>> Get(AllUsersQuery query)
        {
            var users = await _userManager.Users
                .ToListAsync();

            return 
                users.Select(u => new User { ID = u.Id, Name = u.UserName })
                .ToList();
        }
    }
}
