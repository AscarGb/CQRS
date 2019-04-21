using CQRS.Common;
using CQRS.Domain.Commands.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Handlers
{
    public class CheckSecurityUserHandler : IHandler<SaveUserCommand>
    {
        public Task<SaveUserCommand> Handle(SaveUserCommand input)
        {
            //some check

            return Task.FromResult(input);
        }
    }
}
