using System.Threading.Tasks;

namespace DeveloperManagement.Core.Domain.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}