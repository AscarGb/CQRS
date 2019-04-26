using CQRS.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRS.Domain
{
    public sealed class Messages
    {
        readonly IServiceProvider _provider;

        public Messages(IServiceProvider provider)
        {
            _provider = provider;
        }

        public TResult Dispatch<TResult>(ICommand<TResult> command)
        {
            Type type = typeof(ICommandHandler<,>);
            Type[] typeArgs = { command.GetType(), typeof(TResult) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _provider.GetService(handlerType);
            TResult result = handler.Execute((dynamic)command);

            return result;
        }

        public TResponse Dispatch<TResponse>(IQuery<TResponse> query)
        {
            Type type = typeof(IQueryHandler<,>);
            Type[] typeArgs = { query.GetType(), typeof(TResponse) };
            Type handlerType = type.MakeGenericType(typeArgs);

            dynamic handler = _provider.GetService(handlerType);
            TResponse result = handler.Get((dynamic)query);

            return result;
        }
    }
}
