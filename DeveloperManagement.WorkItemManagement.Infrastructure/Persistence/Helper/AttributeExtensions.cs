using System.Reflection;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper
{
    public static class AttributeExtensions
    {
        public static string GetTableName<T>(this T dbEntity) where T : DatabaseEntity
        {
            var attribute = dbEntity.GetType().GetCustomAttribute(typeof(TableNameAttribute), false) as TableNameAttribute;
            return attribute.TableName;
        }
    }
}