using CQRS.Common;
using CQRS.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Commands.Command
{
    public class SaveUserCommand : ICommand<Task<CommandResponse>>
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
