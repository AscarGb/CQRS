using CQRS.Common;
using CQRS.Domain.Commands.Command;
using CQRS.Domain.Handlers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Domain.Pipelines
{
    public class CreateUserPipelineBuilder : PipelineBuilder<SaveUserCommand>
    {
        public CreateUserPipelineBuilder(Pipeline<SaveUserCommand> pipeline) : base(pipeline)
        {
            _pipeline
                .UseHandler<ValidateUserHandler>()
                .UseHandler<CheckSecurityUserHandler>();
        }

        public override Task<SaveUserCommand> ProcessAsync(SaveUserCommand command)
        {
            return _pipeline.ProcessAsync(command);
        }
    }
}