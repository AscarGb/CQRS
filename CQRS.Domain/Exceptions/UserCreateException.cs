using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain.Exceptions
{
    public class UserCreateException : Exception
    {
        public UserCreateException(string message) : base(message)
        {
        }
    }
}
