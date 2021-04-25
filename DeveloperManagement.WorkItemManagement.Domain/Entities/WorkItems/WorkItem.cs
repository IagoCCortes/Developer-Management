using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.Core.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Events.WorkItems;
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
        public string Description { get; private set; }
        public Priority Priority { get; private set; }
        public Link RepoLink { get; private set; }
        private readonly List<Comment> _comments;
        public ReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
        private readonly List<Tag> _tags;
        public ReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();
        private readonly List<RelatedWork> _relatedWorks;
        public ReadOnlyCollection<RelatedWork> RelatedWorks => _relatedWorks.AsReadOnly();
        private readonly List<Attachment> _attachments;
        public ReadOnlyCollection<Attachment> Attachments => _attachments.AsReadOnly();
        public List<DomainEvent> DomainEvents { get; private set; }

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
            DomainEvents = new List<DomainEvent>();
        }


        public void ModifyRepoLink(Link repoLink)
        {
            RepoLink = repoLink;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Link>(nameof(RepoLink), repoLink));
        }

        public void ModifyPriority(Priority priority)
        {
            Priority = priority;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Priority>(nameof(Priority), priority));
        }

        public void ModifyTitle(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException(nameof(Title), "A work item must have a title");

            Title = title;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(Title), title));
        }

        public void ModifyArea(Guid area)
        {
            if (area == Guid.Empty)
                throw new DomainException(nameof(Area), "Invalid area identifier");

            Area = area;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Guid>(nameof(Area), area));
        }

        public void ModifyIteration(Guid? iteration)
        {
            Iteration = iteration;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Guid?>(nameof(Iteration), iteration));
        }

        public void ModifyDescription(string description)
        {
            Description = description;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(Description), description));
        }

        public void AddComment(Comment comment)
        {
            if (comment == null)
                throw new DomainException(nameof(Comments), "A comment may not be empty");

            _comments.Add(comment);
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Comment>(nameof(Comments), comment));
        }

        public void AddTag(Tag tag)
        {
            if (tag == null)
                throw new DomainException(nameof(Tags), "A tag may not be empty");

            _tags.Add(tag);
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Tag>(nameof(Tags), tag));
        }

        public void AddAttachment(Attachment attachment)
        {
            if (attachment == null)
                throw new DomainException(nameof(Attachments), "An attachment may not be empty");

            _attachments.Add(attachment);
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Attachment>(nameof(Attachments), attachment));
        }

        public void AddRelatedWork(RelatedWork relatedWork)
        {
            if (relatedWork.ReferencedWorkItemId.HasValue && relatedWork.ReferencedWorkItemId.Value == Id)
                throw new DomainException(nameof(RelatedWorks),
                    "A work item's relatedWork must not be reference itself");
            if (relatedWork == null)
                throw new DomainException(nameof(RelatedWorks), "A relatedWork must not be null");

            _relatedWorks.Add(relatedWork);
            DomainEvents.Add(new WorkItemFieldModifiedEvent<RelatedWork>(nameof(RelatedWorks), relatedWork));
        }

        public void AssignToMember(Guid memberId)
        {
            if (memberId == Guid.Empty)
                throw new DomainException(nameof(AssignedTo), "A member must be provided");

            AssignedTo = memberId;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Guid>(nameof(AssignedTo), memberId));
        }

        public virtual void ModifyState(WorkItemState state)
        {
            State = state;

            DomainEvents.AddRange(new DomainEvent[]
            {
                new WorkItemFieldModifiedEvent<StateReason>(nameof(StateReason), StateReason),
                new WorkItemFieldModifiedEvent<WorkItemState>(nameof(State), state)
            });

            if (state != WorkItemState.Closed) return;

            var childrenWorkItemsIds =
                _relatedWorks.Where(r => r.LinkType == LinkType.Child && r.ReferencedWorkItemId.HasValue)
                    .Select(r => r.ReferencedWorkItemId.Value);

            DomainEvents.Add(new WorkItemClosedEvent(Id, childrenWorkItemsIds));
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