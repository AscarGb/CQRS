using CQRS.Common;
using CQRS.Data;
using CQRS.Domain.Exceptions;
using CQRS.Domain.Queries.Query;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.Handler
{
    public class OneUserQueryHandler : IQueryHandler<OneUserQuery, Task<User>>
    {        
        UserManager<IdentityUser> _userManager;
        public OneUserQueryHandler(UserManager<IdentityUser> userManager)
        {          
            _userManager = userManager;
        }

        public async Task<User> Get(OneUserQuery query)
        {
            var u = await _userManager.FindByNameAsync(query.Name);

            if (u == null)
                throw new NotFoundException($"user {query.Name} not found");

            return new User { ID = u.Id, Name = u.UserName };
        }
    }
}
