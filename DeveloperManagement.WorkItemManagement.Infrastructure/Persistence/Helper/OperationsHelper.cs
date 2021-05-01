using System.Linq;
using System.Text;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper
{
    public static class OperationsHelper
    {
        public static string BuildInsertStatement(this DatabaseEntity dbEntity)
        {
            var sqlBuilder = new StringBuilder($"INSERT INTO {dbEntity.GetTableName()} (");

            var valuesBuilder = new StringBuilder();

            foreach (var property in dbEntity.GetType().GetProperties())
            {
                var propertyName = property.Name;
                sqlBuilder.Append($"{propertyName},");
                valuesBuilder.Append($"@{propertyName},");
            }

            sqlBuilder.Length--;
            valuesBuilder.Length--;

            sqlBuilder.Append($") VALUES ({valuesBuilder});");

            return sqlBuilder.ToString();
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