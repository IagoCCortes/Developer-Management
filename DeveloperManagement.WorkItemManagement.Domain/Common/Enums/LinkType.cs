using System.ComponentModel;

namespace DeveloperManagement.WorkItemManagement.Domain.Common.Enums
{
    public enum LinkType
    {
        [Description("GitHub Issue")] GitHubIssue = 1,
        Child,
        Duplicate,
        [Description("Duplicate of")] DuplicateOf,
        Parent,
        Predecessor,
        Related,
        Sucessor,
        [Description("Tested by")] TestedBy,
        Tests
    }
}