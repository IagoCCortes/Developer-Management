using System.ComponentModel;

namespace DeveloperManagement.WorkItemManagement.Domain.Enums
{
    public enum WorkItemType
    {
        Bug = 1,
        Epic,
        Feature,
        Issue,
        Task,
        [Description("User Story")] UserStory
    }
}