using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.WorkItems.Commands.UpdateBug
{
    public class UpdateBugCommand : IRequest<Guid>
    {
        
    }

    public class UpdateBugCommandHandler : IRequestHandler<UpdateBugCommand, Guid>
    {
        public Task<Guid> Handle(UpdateBugCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}