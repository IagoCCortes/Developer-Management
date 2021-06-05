using System;

namespace EventBus
{
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        public class SubscriptionInfo
        {
            public Type HandlerType { get; }

            public SubscriptionInfo(Type handlerType)
            {
                HandlerType = handlerType;
            }
        }
    }
}