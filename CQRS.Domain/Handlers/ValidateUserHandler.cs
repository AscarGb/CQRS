using CQRS.Common;
using CQRS.Domain.Commands.Command;
using CQRS.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Handlers
{
    public class ValidateUserHandler : IHandler<SaveUserCommand>
    {
        public Task<SaveUserCommand> Handle(SaveUserCommand input)
        {
            if (string.IsNullOrWhiteSpace(input.User.Name))
            {
                throw new ValidateException("Name");
            }

            if (string.IsNullOrWhiteSpace(input.Password))
            {
                throw new ValidateException("Password");
            }

            return Task.FromResult(input);
        }
    }
}
