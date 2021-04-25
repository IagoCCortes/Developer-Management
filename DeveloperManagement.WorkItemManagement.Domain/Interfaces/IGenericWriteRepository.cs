using System;
using DeveloperManagement.Core.Domain.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Domain.Interfaces
{
    public interface IGenericWriteRepository<in T> where T : IAggregateRoot
    {
        void Insert(T aggregateRoot);
        void Delete(Guid id);
    }
}