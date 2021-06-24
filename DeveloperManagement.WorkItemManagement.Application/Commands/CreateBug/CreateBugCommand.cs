using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Application.Dtos;
using DeveloperManagement.WorkItemManagement.Application.IntegrationEvents;
using DeveloperManagement.WorkItemManagement.Application.IntegrationEvents.Events;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Commands.CreateBug
{
    public class CreateBugCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public Guid? AssignedTo { get; set; }
        public int StateReasonId { get; set; }
        public Guid TeamId { get; set; }
        public Guid? SprintId { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public string RepoLink { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<AttachmentDto> Attachments { get; set; }
        public IEnumerable<RelatedWorkDto> RelatedWorks { get; set; }

        public int OriginalEstimate { get; set; }
        public int Remaining { get; set; }
        public int Completed { get; set; }
        public string IntegratedInBuild { get; set; }
        public int? StoryPoints { get; set; }
        public int SeverityId { get; set; }
        public string SystemInfo { get; set; }
        public string FoundInBuild { get; set; }
    }

    public class CreateBugCommandHandler : IRequestHandler<CreateBugCommand, Guid>
    {
        private readonly IUnitOfWork _uow;
        private readonly IDateTime _dateTime;
        private readonly IMimeTypeMapper _mimeTypeMapper;
        private readonly IWorkItemIntegrationEventService _workItemIntegrationEventService;

        public CreateBugCommandHandler(IUnitOfWork uow, IDateTime dateTime, IMimeTypeMapper mimeTypeMapper,
            IWorkItemIntegrationEventService workItemIntegrationEventService)
        {
            _uow = uow;
            _dateTime = dateTime;
            _mimeTypeMapper = mimeTypeMapper;
            _workItemIntegrationEventService = workItemIntegrationEventService;
        }

        public Task<Guid> Handle(CreateBugCommand request, CancellationToken cancellationToken)
        {
            var effort = new Effort(request.OriginalEstimate, request.Remaining, request.Completed);
            var builder = new Bug.BugBuilder(request.Title, request.TeamId, effort, (Priority) request.PriorityId,
                (StateReason) request.StateReasonId, (Priority) request.SeverityId);

            builder.SetWorkItemOptionalFields(request.Description, request.AssignedTo, request.SprintId,
                String.IsNullOrWhiteSpace(request.RepoLink) ? null : new Link(request.RepoLink));

            builder.SetBugOptionalFields(request.IntegratedInBuild, request.StoryPoints, request.SystemInfo,
                request.FoundInBuild);

            var now = _dateTime.UtcNow;
            foreach (var dto in request.RelatedWorks)
                builder.AddRelatedWork(dto.ToRelatedWork());

            foreach (var dto in request.Attachments)
                builder.AddAttachment(dto.ToAttachment(_mimeTypeMapper.GetMimeType(dto.FileName), now));

            foreach (var tag in request.Tags)
                builder.AddTag(new Tag(tag));

            var bug = builder.BuildWorkItem();

            _uow.BugRepository.Insert(bug);

            if (request.SprintId.HasValue)
                _workItemIntegrationEventService.AddAndSaveEventAsync(
                    new BugCreatedIntegrationEvent(bug.Id, request.OriginalEstimate, request.Remaining,
                        request.Completed, request.SprintId.Value));

            return Task.FromResult(bug.Id);
        }
    }
}