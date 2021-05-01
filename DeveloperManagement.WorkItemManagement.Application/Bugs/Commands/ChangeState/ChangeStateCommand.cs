using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Exceptions;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.ChangeState
{
    public class ChangeStateCommand : IRequest
    {
        public Guid Id { get; set; }
        public int StateId { get; set; }
        public int StateReasonId { get; set; }
    }
    
    public class ChangeStateCommandHandler : IRequestHandler<ChangeStateCommand>
    {
        private readonly IUnitOfWork _uow;

        public ChangeStateCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task<Unit> Handle(ChangeStateCommand request, CancellationToken cancellationToken)
        {
            var bug = await _uow.BugRepository.GetByIdAsync(request.Id);
            if (bug == null) throw new NotFoundException("Bug not found");
            
            bug.ModifyState((WorkItemState) request.StateId, (StateReason) request.StateReasonId);
            _uow.BugRepository.ChangeState(bug);
            await _uow.SaveChangesAsync();
            return Unit.Value;
        }
    }
}