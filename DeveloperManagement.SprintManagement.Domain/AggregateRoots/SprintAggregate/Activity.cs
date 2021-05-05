using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate
{
    public class Activity : Enumeration
    {
        public static Activity Deployment = new Activity(1, nameof(Deployment).ToLowerInvariant());
        public static Activity Design = new Activity(2, nameof(Design).ToLowerInvariant());
        public static Activity Development = new Activity(3, nameof(Development).ToLowerInvariant());
        public static Activity Documentation = new Activity(4, nameof(Documentation).ToLowerInvariant());
        public static Activity Requirements = new Activity(5, nameof(Requirements).ToLowerInvariant());
        public static Activity Testing = new Activity(6, nameof(Testing).ToLowerInvariant());

        public Activity(int id, string name)
            : base(id, name)
        {
        }
    }
}