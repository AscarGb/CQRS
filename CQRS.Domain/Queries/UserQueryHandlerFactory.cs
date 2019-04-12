﻿using CQRS.Common;
using CQRS.Data;
using CQRS.Domain.Queries.Handler;
using CQRS.Domain.Queries.Query;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain.Queries
{
    public class UserQueryHandlerFactory
    {
        UserManager<IdentityUser> _userManager;
        public UserQueryHandlerFactory(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IQueryHandler<AllUsersQuery, IEnumerable<User>> Build(AllUsersQuery query)
        {
            return new AllUsersQueryHandler(_userManager);
        }

        public IQueryHandler<OneUserQuery, User> Build(OneUserQuery query)
        {
            return new OneUserQueryHandler(query, _userManager);
        }
    }
}