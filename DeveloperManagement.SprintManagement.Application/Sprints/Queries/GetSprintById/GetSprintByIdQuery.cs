using System;
using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using MediatR;

namespace DeveloperManagement.SprintManagement.Application.Sprints.Queries.GetSprintById
{
    public class GetSprintByIdQuery : IRequest<Sprint>
    {
        public Guid Id { get; set; }
    }
}