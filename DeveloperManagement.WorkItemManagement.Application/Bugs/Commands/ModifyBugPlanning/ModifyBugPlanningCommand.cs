using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.UpdateBug
{
    public class ModifyBugPlanningCommand : IRequest
    {
        public Guid Id { get; set; }
        public int? StoryPoints { get; set; }
        public int PriorityId { get; set; }
        public int SeverityId { get; set; }
        public int? ActivityId { get; set; }
    }

    public class ModifyBugPlanningCommandHandler : IRequestHandler<ModifyBugPlanningCommand>
    {
        private readonly IUnitOfWork _uow;

        public ModifyBugPlanningCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(ModifyBugPlanningCommand request, CancellationToken cancellationToken)
        {
            var bug = await _uow.BugRepository.GetByIdAsync(request.Id);
            bug.ModifyPlanning(request.StoryPoints, (Priority) request.PriorityId, (Priority) request.SeverityId,
                (Activity?) request.ActivityId);
            return Unit.Value;
        }
    }
}