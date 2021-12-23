using MediatorFromScratch.Mediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediatorFromScratch.Handler
{
    public class GetNameRequest : IRequest<string>
    {
        public string Name { get; set; }
        public class GetNameHandler : IHandler<GetNameRequest, string>
        {
            public Task<string> HandleAsync(GetNameRequest request)
            {
                return Task.FromResult(request.Name);
            }
        }
    }
}
