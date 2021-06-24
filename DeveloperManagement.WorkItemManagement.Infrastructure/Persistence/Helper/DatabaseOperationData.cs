using System;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper
{
    public class DatabaseOperationData
    {
        public Type AggregateRootType { get; }

        public string Sql { get; }
        public DatabaseEntity DbEntity { get; }
        public OperationType OperationType { get; }
        public bool Sent { get; set; }

        public DatabaseOperationData(Type aggregateRootType, string sql, DatabaseEntity dbEntity, OperationType operationType, bool sent = false)
        {
            if (!typeof(IAggregateRoot).IsAssignableFrom(aggregateRootType)) 
                throw new ArgumentException("Type provided is not of an Aggregate root");
            
            AggregateRootType = aggregateRootType;
            Sql = sql;
            DbEntity = dbEntity;
            OperationType = operationType;
            Sent = sent;
        }
    }
}