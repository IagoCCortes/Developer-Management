using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities;
using DeveloperManagement.WorkItemManagement.Domain.Enums;

namespace DeveloperManagement.WorkItemManagement.Domain.Events.WorkItemEvents
{
    public class WorkItemRelatedWorkAddedEvent : DomainEvent
    {
        public LinkType LinkType { get; private set; }
        public string Comment { get; private set; }
        public string Url { get; private set; }
        public Guid? ReferencedWorkItemId { get; private set; }
        public string WorkItemType { get; set; }

        public WorkItemRelatedWorkAddedEvent(RelatedWork relatedWork, string workItemType)
        {
            LinkType = relatedWork.LinkType;
            Comment = relatedWork.Comment;
            Url = relatedWork.Url.Hyperlink;
            ReferencedWorkItemId = relatedWork.ReferencedWorkItemId;
            WorkItemType = workItemType;
        }
    }
}