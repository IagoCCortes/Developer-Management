using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Exceptions;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.ModifyBugPlanning
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

            if (bug == null)
                throw new NotFoundException();
            
            bug.ModifyPlanning(request.StoryPoints, (Priority) request.PriorityId, (Priority) request.SeverityId,
                (Activity?) request.ActivityId);
            _uow.BugRepository.ModifyPlanning(bug);
            await _uow.SaveChangesAsync();
            return Unit.Value;
        }
    }
}