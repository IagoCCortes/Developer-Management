using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItemEvents;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems
{
    public abstract class WorkItem : Entity, IAggregateRoot, IHasDomainEvent
    {
        //when a Work Item is closed all of its children related work must also be finalized
        public string Title { get; private set; }
        public Guid? AssignedTo { get; private set; }
        public WorkItemState State { get; protected set; }
        public StateReason StateReason { get; protected set; }
        public Guid Area { get; private set; }
        public Guid? Iteration { get; private set; }
        public string Description { get; protected set; }
        public Priority Priority { get; protected set; }
        public Link RepoLink { get; private set; }
        private readonly List<Comment> _comments;
        public ReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
        private readonly List<Tag> _tags;
        public ReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        private readonly List<RelatedWork> _relatedWorks;
        public ReadOnlyCollection<RelatedWork> RelatedWorks => _relatedWorks.AsReadOnly();
        private readonly List<Attachment> _attachments;
        public ReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();
        private List<DomainEvent> _domainEvents;

        public List<DomainEvent> DomainEvents => _domainEvents ??= new List<DomainEvent>();

        protected WorkItem()
        {
        }

        protected WorkItem(string title, Guid area, Priority priority)
        {
            Title = title;
            Area = area;
            Priority = priority;
            _tags = new List<Tag>();
            _attachments = new List<Attachment>();
            _relatedWorks = new List<RelatedWork>();
            _comments = new List<Comment>();
        }


        public void ModifyRepoLink(Link repoLink)
        {
            RepoLink = repoLink;
            DomainEvents.Add(new WorkItemRepoLinkModifiedEvent(repoLink.Hyperlink, this.GetType().Name));
        }

        public void ModifyTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException(nameof(Title), "A work item must have a title");

            Title = title;
            DomainEvents.Add(new WorkItemTitleModifiedEvent(title, this.GetType().Name));
        }

        public void ModifyArea(Guid area)
        {
            if (area == Guid.Empty)
                throw new DomainException(nameof(Area), "Invalid area identifier");

            Area = area;
            DomainEvents.Add(new WorkItemAreaModifiedEvent(area, this.GetType().Name));
        }

        public void ModifyIteration(Guid? iteration)
        {
            Iteration = iteration;
            DomainEvents.Add(new WorkItemIterationModifiedEvent(iteration, this.GetType().Name));
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new DomainException(nameof(Comments), "A comment may not be empty");

            _comments.Add(comment);
            DomainEvents.Add(new WorkItemCommentAddedEvent(comment.Text, this.GetType().Name));
        }

        public void AddTag(Tag tag)
        {
            if (tag == null)
                throw new DomainException(nameof(Tags), "A tag may not be empty");

            _tags.Add(tag);
            DomainEvents.Add(new WorkItemTagAddedEvent(tag, this.GetType().Name));
        }

        public void AddAttachment(Attachment attachment)
        {
            if (attachment == null)
                throw new DomainException(nameof(Attachments), "An attachment may not be empty");

            _attachments.Add(attachment);
            DomainEvents.Add(new WorkItemAttachmentAddedEvent(attachment, this.GetType().Name));
        }

        public void AddRelatedWork(RelatedWork relatedWork)
        {
            if (relatedWork.ReferencedWorkItemId.HasValue && relatedWork.ReferencedWorkItemId.Value == Id)
                throw new DomainException(nameof(RelatedWorks),
                    "A work item cannot be related to itself");
            if (relatedWork == null)
                throw new DomainException(nameof(RelatedWorks), "A relatedWork must not be null");

            _relatedWorks.Add(relatedWork);
            DomainEvents.Add(new WorkItemRelatedWorkAddedEvent(relatedWork, this.GetType().Name));
        }
        
        public void RemoveRelatedWork(){}

        public void AssignToMember(Guid memberId)
        {
            if (memberId == Guid.Empty)
                throw new DomainException(nameof(AssignedTo), "A member must be provided");

            AssignedTo = memberId;
            DomainEvents.Add(new WorkItemAssignedToModifiedEvent(memberId, this.GetType().Name));
        }

        public virtual void ModifyState(WorkItemState state)
        {
            State = state;

            DomainEvents.Add(new WorkItemStateModified(state, StateReason, this.GetType().Name));

            if (state != WorkItemState.Closed) return;

            var childrenWorkItemsIds =
                _relatedWorks.Where(r => r.LinkType == LinkType.Child && r.ReferencedWorkItemId.HasValue)
                    .Select(r => r.ReferencedWorkItemId.Value);

            DomainEvents.Add(new WorkItemClosedEvent(Id, childrenWorkItemsIds, this.GetType().Name));
        }

        public abstract class WorkItemBuilder<T> where T : WorkItem
        {
            protected T WorkItem;

            public WorkItemBuilder<T> SetWorkItemOptionalFields(string description, Guid? assignedTo, Guid? iteration, Link repoLink)
            {
                WorkItem.Description = description;
                WorkItem.AssignedTo = assignedTo;
                WorkItem.Iteration = iteration;
                WorkItem.RepoLink = repoLink;
                return this;
            }
            
            public WorkItemBuilder<T> AddComment(Comment comment)
            {
                WorkItem._comments.Add(comment);
                return this;
            }
            
            public WorkItemBuilder<T> AddTag(Tag tag)
            {
                WorkItem._tags.Add(tag);
                return this;
            }
            
            public WorkItemBuilder<T> AddAttachment(Attachment attachment)
            {
                WorkItem._attachments.Add(attachment);
                return this;
            }
            
            public WorkItemBuilder<T> AddRelatedWork(RelatedWork relatedWork)
            {
                WorkItem._relatedWorks.Add(relatedWork);
                return this;
            }

            public abstract T BuildWorkItem();
        }
    }
}