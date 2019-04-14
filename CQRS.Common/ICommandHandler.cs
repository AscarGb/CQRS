using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Common
{
    public interface ICommandHandler<in TCommand,out TResult> where TCommand : ICommand<TResult>
    {
        TResult Execute();
    }
}
