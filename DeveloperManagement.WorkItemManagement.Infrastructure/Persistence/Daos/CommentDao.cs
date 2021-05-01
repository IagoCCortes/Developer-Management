using System;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.CommentAggregate;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;
using BindingFlags = System.Reflection.BindingFlags;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("Comment")]
    public class CommentDao : DatabaseEntity
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid WorkItemId { get; set; }

        public Comment ToComment()
        {
            var commentType = typeof(Comment);
            var comment = (Comment)commentType
                .GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, new Type[] { }, null)
                .Invoke(null);

            commentType.GetProperty("Text").SetValue(comment, Text);
            commentType.GetProperty("WorkItemId").SetValue(comment, WorkItemId);
            commentType.GetProperty("Created").SetValue(comment, Created);
            commentType.GetProperty("CreatedBy").SetValue(comment, CreatedBy);

            return comment;
        }
    }
}