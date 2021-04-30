using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Exceptions;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.SpecifyBugInfo
{
    public class SpecifyBugInfoCommand : IRequest
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string SystemInfo { get; set; }
        public string FoundInBuild { get; set; }
        public string IntegratedInBuild { get; set; }
    }
    
    public class SpecifyBugInfoCommandHandler : IRequestHandler<SpecifyBugInfoCommand>
    {
        private readonly IUnitOfWork _uow;

        public SpecifyBugInfoCommandHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task<Unit> Handle(SpecifyBugInfoCommand request, CancellationToken cancellationToken)
        {
            var bug = await _uow.BugRepository.GetByIdAsync(request.Id);
            if (bug == null)
                throw new NotFoundException();
            
            _uow.BugRepository.SpecifyInfo(bug);
            await _uow.SaveChangesAsync();
            
            return Unit.Value;
        }
    }
}