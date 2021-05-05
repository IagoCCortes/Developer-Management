using System;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using MediatR;

namespace DeveloperManagement.SprintManagement.Application.Sprints.Queries.GetSprintById
{
    public class GetSprintByIdQuery : IRequest<Sprint>
    {
        public Guid Id { get; set; }
    }
    
    public class GetSprintByIdQueryHandler : IRequestHandler<GetSprintByIdQuery, Sprint>
    {
        private readonly ISprintRepository _repository;

        public GetSprintByIdQueryHandler(ISprintRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Sprint> Handle(GetSprintByIdQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAsync(request.Id);
        }
    }
}