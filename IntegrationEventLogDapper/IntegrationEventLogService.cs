using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Dapper;
using EventBus;
using EventBus.Events;
using MySql.Data.MySqlClient;

namespace IntegrationEventLogDapper
{
    public class IntegrationEventLogService : IIntegrationEventLogService, IDisposable
    {
        private readonly string _connectionString;

        public IntegrationEventLogService(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<IntegrationEventLogEntry>> RetrieveEventLogsPendingToPublishAsync(
            Guid transactionId, List<Type> eventTypes)
        {
            var tid = transactionId.ToString();

            var sqlConnection = new MySqlConnection(_connectionString);
            await sqlConnection.OpenAsync();
            using var connection = sqlConnection;
            var result = await connection.QueryAsync<IntegrationEventLogEntry>(
                "SELECT Id, EventTypeName, EventStateId, TimesSent, CreationTime, Content " +
                "FROM IntegrationEventLogEntry " +
                "WHERE EventStateId = @EventStateId AND TransactionId = @TransactionId;",
                new {EventStateId = (int) EventState.NotPublished, TransactionId = transactionId});

            if (result != null && result.Any())
            {
                return result.OrderBy(o => o.CreationTime)
                    .Select(e => e.DeserializeJsonContent(eventTypes.Find(t => t.Name == e.EventTypeShortName)));
            }

            return new List<IntegrationEventLogEntry>();
        }

        public async Task MarkEventAsPublishedAsync(Guid eventId) => await UpdateEventStatus(eventId, EventState.Published);

        public async Task MarkEventAsInProgressAsync(Guid eventId) => await UpdateEventStatus(eventId, EventState.InProgress);

        public async Task MarkEventAsFailedAsync(Guid eventId) => await UpdateEventStatus(eventId, EventState.PublishedFailed);

        private async Task<int> UpdateEventStatus(Guid eventId, EventState status)
        {
            var sqlConnection = new MySqlConnection(_connectionString);
            await sqlConnection.OpenAsync();
            using var connection = sqlConnection;

            return await connection.ExecuteAsync(
                "UPDATE IntegrationEventLogEntry SET EventStateId = @stateId, "+
                   "TimesSent = CASE WHEN EventStateId = @inProgress THEN TimesSent + 1 ELSE TimesSent END " +
                   "WHERE Id = @id;",
                new {stateId = (int) status, inProgress = (int) EventState.InProgress, id = eventId});
        }

        public void Dispose()
        {
        }
    }
}