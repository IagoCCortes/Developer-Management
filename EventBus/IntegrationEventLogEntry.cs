using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json;
using EventBus.Events;

namespace EventBus
{
    public class IntegrationEventLogEntry
    {
        private IntegrationEventLogEntry() { }
        public IntegrationEventLogEntry(IntegrationEvent @event)
        {
            Id = @event.Id;
            CreationTime = @event.CreationDate;
            EventTypeName = @event.GetType().FullName;                     
            Content = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
            {
                WriteIndented = true
            });
            State = EventState.NotPublished;
            TimesSent = 0;
        }
        public Guid Id { get; private set; }
        public string EventTypeName { get; private set; }
        [NotMapped]
        public string EventTypeShortName => EventTypeName.Split('.')?.Last();
        [NotMapped]
        public IntegrationEvent IntegrationEvent { get; private set; }
        public EventState State { get; set; }
        public int EventStateId => (int) State;
        public int TimesSent { get; set; }
        public DateTime CreationTime { get; private set; }
        public string Content { get; private set; }
        public string TransactionId { get; private set; }

        public void SetTransactionId(Guid transactionId) => TransactionId = transactionId.ToString();

        public IntegrationEventLogEntry DeserializeJsonContent(Type type)
        {            
            IntegrationEvent = JsonSerializer.Deserialize(Content, type, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) as IntegrationEvent;
            return this;
        }
    }
}