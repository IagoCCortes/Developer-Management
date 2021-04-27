using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.Bug
{
    public class BugInfoModifiedEvent : DomainEvent
    {
        public string Description { get; set; }
        public string SystemInfo { get; set; }
        public string FoundInBuild { get; set; }
        public string IntegratedInBuild { get; set; }

        public BugInfoModifiedEvent(string description, string systemInfo, string foundInBuild, string integratedInBuild)
        {
            Description = description;
            SystemInfo = systemInfo;
            FoundInBuild = foundInBuild;
            IntegratedInBuild = integratedInBuild;
        }
    }
}