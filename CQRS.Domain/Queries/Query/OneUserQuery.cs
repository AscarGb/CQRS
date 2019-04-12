using CQRS.Common;
using CQRS.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain.Queries.Query
{
    public class OneUserQuery : IQuery<User>
    {
        public string Name { get; set; }
    }
}
