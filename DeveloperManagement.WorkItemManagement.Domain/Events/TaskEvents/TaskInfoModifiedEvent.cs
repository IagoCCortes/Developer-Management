using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.TaskEvents
{
    public class TaskInfoModifiedEvent : DomainEvent
    {
        public string Description { get; set; }
        public string IntegratedInBuild { get; set; }

        public TaskInfoModifiedEvent(string description, string integratedInBuild)
        {
            Description = description;
            IntegratedInBuild = integratedInBuild;
        }
    }
}