using CQRS.Common;
using CQRS.Domain.Commands.Command;
using CQRS.Domain.Commands.Handler;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain.Commands
{
    public class UserCommandHandlerFactory
    {
        UserManager<IdentityUser> _userManager;

        public UserCommandHandlerFactory(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public ICommandHandler<SaveUserCommand, CommandResponse> Build(SaveUserCommand command)
        {
            return new SaveUserCommandHandler(command, _userManager);
        }
    }
}
