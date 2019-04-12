using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Common
{
    public interface IQuery<out TResponse> { }
}
