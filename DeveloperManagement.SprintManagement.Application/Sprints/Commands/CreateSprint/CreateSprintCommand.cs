using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using MediatR;

namespace DeveloperManagement.SprintManagement.Application.Sprints.Commands.CreateSprint
{
    public class CreateSprintCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime FinalDate { get; set; }
        public Guid TeamId { get; set; }
    }
    
    public class CreateSprintCommandHandler : IRequestHandler<CreateSprintCommand, Guid>
    {
        private readonly ISprintRepository _repository;

        public CreateSprintCommandHandler(ISprintRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Guid> Handle(CreateSprintCommand request, CancellationToken cancellationToken)
        {
            var sprint = new Sprint(request.Name, new Period(request.InitialDate, request.FinalDate), request.TeamId);
            _repository.Add(sprint);
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return sprint.Id;
        }
    }
}