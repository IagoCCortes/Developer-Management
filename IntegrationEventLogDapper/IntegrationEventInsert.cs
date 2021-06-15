using EventBus;

namespace IntegrationEventLogDapper
{
    public class IntegrationEventInsert
    {
        public string Sql { get; private set; }
        public IntegrationEventLogEntry IntegrationEventLogEntry { get; private set; }

        public IntegrationEventInsert(string sql, IntegrationEventLogEntry integrationEventLogEntry)
        {
            Sql = sql;
            IntegrationEventLogEntry = integrationEventLogEntry;
        }
    }
}