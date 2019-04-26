using CQRS.Common;
using CQRS.Domain.Commands.Command;
using CQRS.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.Handler
{
    public class SaveUserCommandHandler : ICommandHandler<SaveUserCommand, Task<CommandResponse>>
    {        
        UserManager<IdentityUser> _userManager;
        public SaveUserCommandHandler(UserManager<IdentityUser> userManager)
        {            
            _userManager = userManager;

        }
        public async Task<CommandResponse> Execute(SaveUserCommand command)
        {
            IdentityUser user = new IdentityUser { UserName = command.User.Name };

            var r = await _userManager.CreateAsync(user, command.Password);
            if (r.Succeeded)
            {
                return new CommandResponse { ID = user.Id, Success = true };
            }
            else
            {
                throw new UserCreateException(string.Format("Code {0} Description {1}", r.Errors.First().Code, r.Errors.First().Description));
            }
        }
    }
}
