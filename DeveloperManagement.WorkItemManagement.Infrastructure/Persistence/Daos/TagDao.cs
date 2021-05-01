using System;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("Tag")]
    public class TagDao : DatabaseEntity
    {
        public string Text { get; set; }
        public Guid WorkItemId { get; set; }

        public Tag ToTag() => new Tag(Text);
    }
}