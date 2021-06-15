using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper
{
    public record DatabaseOperationData(string Sql, DatabaseEntity DbEntity, OperationType OperationType);
}