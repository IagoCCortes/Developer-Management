using System;
using System.Linq.Expressions;
using System.Reflection;
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

        private static readonly Type CommentType = typeof(Comment);
        private static readonly ConstructorInfo CommentConstructorInfo =
            CommentType.GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, null, Array.Empty<Type>(), null);

        private static readonly PropertyInfo TextInfo = CommentType.GetProperty("Text");
        private static readonly PropertyInfo WorkItemIdInfo = CommentType.GetProperty("WorkItemId");
        private static readonly PropertyInfo CreatedInfo = CommentType.GetProperty("Created");
        private static readonly PropertyInfo CreatedByInfo = CommentType.GetProperty("CreatedBy");

        private static readonly Func<Comment> CommentConstructor =
            Expression.Lambda<Func<Comment>>(Expression.New(CommentConstructorInfo)).Compile();

        private static readonly Action<Comment, string> SetTextDelegate =
            (Action<Comment, string>) Delegate.CreateDelegate(typeof(Action<Comment, string>), TextInfo.GetSetMethod(true)!);
        private static readonly Action<Comment, Guid> SetWorkItemIdDelegate =
            (Action<Comment, Guid>) Delegate.CreateDelegate(typeof(Action<Comment, Guid>), WorkItemIdInfo.GetSetMethod(true)!);
        private static readonly Action<Comment, DateTime> SetCreatedDelegate =
            (Action<Comment, DateTime>) Delegate.CreateDelegate(typeof(Action<Comment, DateTime>), CreatedInfo.GetSetMethod(true)!);
        private static readonly Action<Comment, string> SetCreatedByDelegate =
            (Action<Comment, string>) Delegate.CreateDelegate(typeof(Action<Comment, string>), CreatedByInfo.GetSetMethod(true)!);

        public Comment ToComment()
        {
            var comment = CommentConstructor();

            SetTextDelegate(comment, Text);
            SetWorkItemIdDelegate(comment, WorkItemId);
            SetCreatedDelegate(comment, Created);
            SetCreatedByDelegate(comment, CreatedBy);

            return comment;
        }
    }
}