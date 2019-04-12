using CQRS.Common;
using CQRS.Domain.Commands.Command;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.Handler
{
    public class SaveUserCommandHandler : ICommandHandler<SaveUserCommand, CommandResponse>
    {
        private readonly SaveUserCommand _command;
        UserManager<IdentityUser> _userManager;
        public SaveUserCommandHandler(SaveUserCommand command, UserManager<IdentityUser> userManager)
        {
            _command = command;
            _userManager = userManager;

        }
        public async Task<CommandResponse> ExecuteAsync()
        {
            IdentityUser user = new IdentityUser { UserName = _command.User.Name };

            var r = await _userManager.CreateAsync(user, "123!@#qweQWE");
            if (r.Succeeded)
            {
                return new CommandResponse { ID = user.Id, Success = true };
            }
            else
            {
                return new CommandResponse
                {
                    Success = false,
                    Message = string.Format("Code {0} Description {1}", r.Errors.First().Code, r.Errors.First().Description)
                };
            }
        }
    }
}
