using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using MediatR;

namespace DeveloperManagement.SprintManagement.Application.Commands.AddWorkItemToSprint
{
    public class AddWorkItemToSprintCommand : IRequest
    {
        public Guid BugId { get; set; }
        public Guid SprintId { get; set; }
        public int OriginalEstimate { get; set; }
        public int Remaining { get; set; }
        public int Completed { get; set; }
    }
    
    public class AddWorkItemToSprintCommandHandler : IRequestHandler<AddWorkItemToSprintCommand>
    {
        private readonly ISprintRepository _repository;

        public AddWorkItemToSprintCommandHandler(ISprintRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Unit> Handle(AddWorkItemToSprintCommand request, CancellationToken cancellationToken)
        {
            var sprint = await _repository.GetAsync(request.SprintId);
            var effort = new Effort(request.OriginalEstimate, request.Remaining, request.Completed);
            
            sprint.AddWorkItem(new WorkItem(request.BugId, effort));

            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}