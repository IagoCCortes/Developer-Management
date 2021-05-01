using System;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate.Events
{
    public class WorkItemAttachmentAddedEvent : DomainEvent
    {
        public string Path { get; set; }
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public DateTime Created { get; set; }
        public string WorkItemType { get; set; }

        public WorkItemAttachmentAddedEvent(Attachment attachment, string workItemType)
        {
            Path = attachment.Path;
            FileName = attachment.FileName;
            MimeType = attachment.MimeType;
            Created = attachment.Created;
            WorkItemType = workItemType;
        }
    }
}