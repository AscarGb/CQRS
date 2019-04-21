using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain.Exceptions
{
    public class ValidateException : Exception
    {
        public ValidateException(string message) : base(message)
        {
        }
    }
}
