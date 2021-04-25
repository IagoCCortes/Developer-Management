using System.Data;
using System.Threading.Tasks;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces
{
    public interface IDapperConnectionFactory
    {
        Task<IDbConnection> CreateConnectionAsync();
    }
}