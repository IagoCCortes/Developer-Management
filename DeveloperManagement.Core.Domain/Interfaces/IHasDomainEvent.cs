using System.Collections.Generic;

namespace DeveloperManagement.Core.Domain.Interfaces
{
    public interface IHasDomainEvent
    {
        List<DomainEvent> DomainEvents { get; }
    }
}