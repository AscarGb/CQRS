using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Common
{
    public abstract class PipelineBuilder<T>
    {
        protected Pipeline<T> _pipeline;
        public PipelineBuilder(Pipeline<T> pipeline)
        {
            _pipeline = pipeline;
        }
        public abstract Task<T> ProcessAsync(T input);
    }
}
