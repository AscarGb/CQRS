using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Common
{
    public interface IHandler<T>
    {
       Task<T> Handle(T input);
    }
}