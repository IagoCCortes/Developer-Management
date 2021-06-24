using System;
using System.Collections.Generic;
using System.Text;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using EventBus;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper
{
    public static class OperationsHelper
    {
        private static readonly Dictionary<Type, string> InsertStatementCache = new Dictionary<Type, string>();
        
        public static string BuildInsertStatement(this DatabaseEntity dbEntity)
        {
            var type = dbEntity.GetType();

            if (InsertStatementCache.TryGetValue(dbEntity.GetType(), out var statement))
                return statement;
            
            var sqlBuilder = new StringBuilder($"INSERT INTO {dbEntity.GetTableName()} (");

            var valuesBuilder = new StringBuilder();

            foreach (var property in type.GetProperties())
            {
                var propertyName = property.Name;
                sqlBuilder.Append($"{propertyName},");
                valuesBuilder.Append($"@{propertyName},");
            }

            sqlBuilder.Length--;
            valuesBuilder.Length--;

            var builtStatement = sqlBuilder.Append($") VALUES ({valuesBuilder});").ToString();
            
            InsertStatementCache.Add(type, builtStatement);

            return builtStatement;
        }

        public static string BuildIntegrationEventLogEntryInsertStatement(this IntegrationEventLogEntry eventLogEntry)
        {
            return 
                "INSERT INTO IntegrationEventLogEntry " +
                    "(Id, EventTypeName, EventStateId, TimesSent, CreationTime, Content, TransactionId) " +
                "VALUES (@Id, @EventTypeName, @EventStateId, @TimesSent, @CreationTime, @Content, @TransactionId);";
        }

        public static string BuildUpdateStatement(string tableName, string conditionColumn, params string[] columns)
        {
            var sb = new StringBuilder($"UPDATE {tableName} SET ");
            foreach (var column in columns)
                sb.Append($"{column} = @{column}, ");
            sb.Append($"LastModified = @LastModified, LastModifiedBy = @LastModifiedBy ");
            return sb.Append($"WHERE {conditionColumn} = @{conditionColumn}").ToString();
        }

        public static string BuildDeleteStatement(this DatabaseEntity dbEntity) =>
            $"DELETE FROM {dbEntity.GetTableName()} WHERE id = @id";
    }
}