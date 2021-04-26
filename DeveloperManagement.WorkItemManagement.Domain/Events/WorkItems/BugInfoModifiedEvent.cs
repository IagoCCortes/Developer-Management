using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems
{
    public class BugInfoModifiedEvent : DomainEvent
    {
        public string ReproSteps { get; set; }
        public string SystemInfo { get; set; }
        public string FoundInBuild { get; set; }
        public string IntegratedInBuild { get; set; }

        public BugInfoModifiedEvent(string reproSteps, string systemInfo, string foundInBuild, string integratedInBuild)
        {
            ReproSteps = reproSteps;
            SystemInfo = systemInfo;
            FoundInBuild = foundInBuild;
            IntegratedInBuild = integratedInBuild;
        }
    }
}