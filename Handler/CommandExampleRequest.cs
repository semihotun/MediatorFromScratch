using MediatorFromScratch.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatorFromScratch.Handler
{
    public class CommandExampleRequest : IRequest<bool>
    {
        public string Name { get; set; }
        public class CommandExampleHandler : IHandler<CommandExampleRequest, bool>
        {
            public Task<bool> HandleAsync(CommandExampleRequest request)
            {
                return Task.FromResult(true);
            }
        }
    }
}
