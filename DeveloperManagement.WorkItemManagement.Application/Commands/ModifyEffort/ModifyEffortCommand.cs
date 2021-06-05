using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Exceptions;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Commands.ModifyEffort
{
    public class ModifyEffortCommand : IRequest
    {
        public Guid Id { get; set; }
        public int Remaining { get; set; }
        public int Completed { get; set; }
    }

    public class ModifyEffortCommandHandler : IRequestHandler<ModifyEffortCommand>
    {
        private readonly IUnitOfWork _uow;

        public ModifyEffortCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public async Task<Unit> Handle(ModifyEffortCommand request, CancellationToken cancellationToken)
        {
            var bug = await _uow.BugRepository.GetByIdAsync(request.Id);
            if (bug == null)
                throw new NotFoundException("Bug not found");
            bug.ModifyEffort(request.Remaining, request.Completed);

            _uow.BugRepository.ModifyEffort(bug);
            await _uow.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}