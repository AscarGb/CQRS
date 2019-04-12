using CQRS.Common;
using CQRS.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain.Queries.Query
{
    public class AllUsersQuery : IQuery<IEnumerable<User>>
    {
    }
}
