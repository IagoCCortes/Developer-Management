using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Queries.GetBugById
{
    public class GetBugByIdQuery : IRequest<Bug>
    {
        public Guid Id { get; set; }      
    }
    
    public class GetBugByIdQueryHandler : IRequestHandler<GetBugByIdQuery, Bug>
    {
        private readonly IUnitOfWork _uow;

        public GetBugByIdQueryHandler(IUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task<Bug> Handle(GetBugByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.BugRepository.GetByIdAsync(request.Id);
        }
    }
}