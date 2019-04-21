using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace CQRS.Common
{
    public sealed class Pipeline<T>
    {
        private IServiceProvider _currentServiceProvider;

        readonly List<IHandler<T>> handlers = new List<IHandler<T>>();

        public Pipeline(IServiceProvider currentServiceProvider)
        {
            _currentServiceProvider = currentServiceProvider;
        }

        public Pipeline<T> UseHandler<TH>() where TH : IHandler<T>
        {
            var handler = _currentServiceProvider.GetRequiredService<TH>();
            handlers.Add(handler);
            return this;
        }

        public async Task<T> ProcessAsync(T input)
        {
            foreach (var filter in handlers)
            {
                input = await filter.Handle(input);
            }

            return input;
        }
    }
}