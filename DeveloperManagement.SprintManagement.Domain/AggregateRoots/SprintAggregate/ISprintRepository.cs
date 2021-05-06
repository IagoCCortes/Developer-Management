using System;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Domain.Common.Interfaces;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate
{
    public interface ISprintRepository : IRepository<Sprint>
    {
        Sprint Add(Sprint order);

        void AddCapacity();

        Task<Sprint> GetAsync(Guid sprintId);
    }
}