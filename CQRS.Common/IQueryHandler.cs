using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Common
{
    public interface IQueryHandler<in TQuery, out TResponse>         
        where TQuery : IQuery<TResponse>
    {
        TResponse Get();
    }
}
