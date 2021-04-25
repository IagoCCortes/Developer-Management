using System;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos
{
    [TableName("Attachment")]
    public class AttachmentDao : DatabaseEntity
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public Guid WorkItemId { get; set; }
        
        public Attachment ToAttachment() => new Attachment(Path, FileName, MimeType, Created);
    }
}