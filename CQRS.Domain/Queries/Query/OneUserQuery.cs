using CQRS.Common;
using CQRS.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Queries.Query
{
    public class OneUserQuery : IQuery<Task<User>>
    {
        public string Name { get; set; }
    }
}
