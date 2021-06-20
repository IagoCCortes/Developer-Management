using System;
using System.Collections.Generic;
using EventBus.Abstractions;
using EventBus.Events;

namespace EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }
        
        void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;
        
        bool HasSubscriptionsForEvent(string eventName);
        
        Type GetEventTypeByName(string eventName);
        
        void Clear();
        
        IEnumerable<InMemoryEventBusSubscriptionsManager.SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;
        
        IEnumerable<InMemoryEventBusSubscriptionsManager.SubscriptionInfo> GetHandlersForEvent(string eventName);
        
        string GetEventKey<T>();
    }
}