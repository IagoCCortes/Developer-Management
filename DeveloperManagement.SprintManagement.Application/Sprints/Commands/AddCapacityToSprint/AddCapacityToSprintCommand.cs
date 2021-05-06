using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Exceptions;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using MediatR;

namespace DeveloperManagement.SprintManagement.Application.Sprints.Commands.AddCapacityToSprint
{
    public class AddCapacityToSprintCommand : IRequest
    {
        public Guid Id { get; set; }
        public int ActivityId { get; set; }
        public int CapacityPerDay { get; set; }
        public List<PeriodDto> DaysOff { get; set; }
    }

    public class AddCapacityToSprintCommandHandler : IRequestHandler<AddCapacityToSprintCommand>
    {
        private readonly ISprintRepository _repository;

        public AddCapacityToSprintCommandHandler(ISprintRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AddCapacityToSprintCommand request, CancellationToken cancellationToken)
        {
            var sprint = await _repository.GetAsync(request.Id);
            if (sprint == null) throw new NotFoundException("Sprint not found");

            var daysOff = request.DaysOff.Select(d => new Period(d.InitialDateTime, d.FinalDateTime)).ToList();

            var capacity = new Capacity(Enumeration.FromValue<Activity>(request.ActivityId), daysOff,
                request.CapacityPerDay);

            sprint.AddCapacity(capacity);
            _repository.AddCapacity();
            await _repository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}