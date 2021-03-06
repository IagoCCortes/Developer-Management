using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Queries.GetBugById
{
    public class GetBugByIdQuery : IRequest<Bug>
    {
        public Guid Id { get; set; }      
    }
    
    public class GetBugByIdQueryHandler : IRequestHandler<GetBugByIdQuery, Bug>
    {
        private readonly IDomainUnitOfWork _uow;

        public GetBugByIdQueryHandler(IDomainUnitOfWork uow)
        {
            _uow = uow;
        }
        
        public async Task<Bug> Handle(GetBugByIdQuery request, CancellationToken cancellationToken)
        {
            return await _uow.BugRepository.GetByIdAsync(request.Id);
        }
    }
}