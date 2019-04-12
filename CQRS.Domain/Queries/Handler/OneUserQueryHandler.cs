using CQRS.Common;
using CQRS.Data;
using CQRS.Domain.Queries.Query;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.Handler
{
    public class OneUserQueryHandler : IQueryHandler<OneUserQuery, User>
    {
        private readonly OneUserQuery _query;
        UserManager<IdentityUser> _userManager;
        public OneUserQueryHandler(OneUserQuery query, UserManager<IdentityUser> userManager)
        {
            _query = query;
            _userManager = userManager;
        }

        public async Task<User> GetAsync()
        {
            var u = await _userManager.FindByNameAsync(_query.Name);

            if (u == null)
                return null;

            return new User { ID = u.Id, Name = u.UserName };
        }
    }
}
