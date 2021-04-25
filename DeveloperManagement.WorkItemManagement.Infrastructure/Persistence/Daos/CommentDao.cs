using System;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("Comment")]
    public class CommentDao : DatabaseEntity
    {
        public string Text { get; set; }
        public Guid WorkItemId { get; set; }
        
        public Comment ToComment() => new Comment(Text, Created);
    }
}