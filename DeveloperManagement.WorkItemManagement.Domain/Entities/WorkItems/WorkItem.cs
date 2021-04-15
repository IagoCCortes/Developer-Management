﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public Guid AssignedTo { get; private set; }
        public WorkItemState State { get; protected set; }
        public Guid Area { get; private set; }
        public Guid Iteration { get; private set; }
        public string Description { get; private set; }
        public Priority Priority { get; private set; }
        public Link RepoLink { get; private set; }
        private List<string> _comments;
        public ReadOnlyCollection<string> Comments => _comments.AsReadOnly();
        private List<string> _tags;
        public ReadOnlyCollection<string> Tags => _tags.AsReadOnly();
        private List<RelatedWork> _relatedWorks;
        public ReadOnlyCollection<RelatedWork> RelatedWorks => _relatedWorks.AsReadOnly();
        private List<string> _attachments;
        public ReadOnlyCollection<string> Attachments => _attachments.AsReadOnly();
        public StateReason StateReason { get; protected set; }
        public List<DomainEvent> DomainEvents { get; private set; }

        protected WorkItem(string title, Guid area, Guid iteration, Priority priority)
        {
            Title = title;
            Area = area;
            Iteration = iteration;
            Priority = priority;
            _tags = new List<string>();
            _attachments = new List<string>();
            _relatedWorks = new List<RelatedWork>();
            _comments = new List<string>();
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

        public void ModifyIteration(Guid iteration)
        {
            if (iteration == Guid.Empty)
                throw new DomainException(nameof(Iteration), "Invalid Iteration identifier");

            Iteration = iteration;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<Guid>(nameof(Iteration), iteration));
        }

        public void ModifyDescription(string description)
        {
            Description = description;
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(Description), description));
        }

        public void AddComment(string comment)
        {
            if (string.IsNullOrWhiteSpace(comment))
                throw new DomainException(nameof(Comments), "A comment may not be empty");

            _comments.Add(comment);
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(Comments), comment));
        }

        public void AddTag(string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
                throw new DomainException(nameof(Tags), "A tag may not be empty");

            _tags.Add(tag);
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(Tags), tag));
        }

        public void AddAttachment(string attachment)
        {
            if (string.IsNullOrWhiteSpace(attachment))
                throw new DomainException(nameof(Attachments), "An attachment may not be empty");

            _attachments.Add(attachment);
            DomainEvents.Add(new WorkItemFieldModifiedEvent<string>(nameof(Attachments), attachment));
        }

        public void AddRelatedWork(RelatedWork relatedWork)
        {
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
    }
}