using CQRS.Common;
using CQRS.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain.Commands.Command
{
    public class SaveUserCommand : ICommand<CommandResponse>
    {
        public User User { get; private set; }
        public string Password { get; private set; }
        public SaveUserCommand(User user, string password)
        {
            User = user;
            Password = password;
        }
    }
}
