using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DeveloperManagement.Core.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Application.Dtos;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;
using MediatR;

namespace DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.AddBug
{
    public class CreateBugCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public Guid? AssignedTo { get; set; }
        public int StateReasonId { get; set; }
        public Guid AreaId { get; set; }
        public Guid? IterationId { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public string RepoLink { get; set; }
        public IEnumerable<string> Comments { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<AttachmentDto> Attachments { get; set; }
        public IEnumerable<RelatedWorkDto> RelatedWorks { get; set; }

        public int? OriginalEstimate { get; set; }
        public int? Remaining { get; set; }
        public int? Completed { get; set; }
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

        public CreateBugCommandHandler(IUnitOfWork uow, IDateTime dateTime, IMimeTypeMapper mimeTypeMapper)
        {
            _uow = uow;
            _dateTime = dateTime;
            _mimeTypeMapper = mimeTypeMapper;
        }

        public async Task<Guid> Handle(CreateBugCommand request, CancellationToken cancellationToken)
        {
            var builder = new Bug.BugBuilder(request.Title, request.AreaId, (Priority) request.PriorityId,
                (StateReason) request.StateReasonId, (Priority) request.SeverityId);

            builder.SetWorkItemOptionalFields(request.Description, request.AssignedTo, request.IterationId,
                String.IsNullOrWhiteSpace(request.RepoLink) ? null : new Link(request.RepoLink));

            builder.SetBugOptionalFields(
                request.OriginalEstimate.HasValue
                    ? new Effort(request.OriginalEstimate.Value, request.Remaining!.Value, request.Completed!.Value)
                    : null, request.IntegratedInBuild, request.StoryPoints, request.SystemInfo, request.FoundInBuild);

            var now = _dateTime.UtcNow;
            foreach (var dto in request.RelatedWorks)
                builder.AddRelatedWork(dto.ToRelatedWork());

            foreach (var dto in request.Attachments)
                builder.AddAttachment(dto.ToAttachment(_mimeTypeMapper.GetMimeType(dto.FileName), now));

            foreach (var comment in request.Comments)
                builder.AddComment(new Comment(comment));

            foreach (var tag in request.Tags)
                builder.AddTag(new Tag(tag));

            var bug = builder.BuildWorkItem();

            _uow.BugRepository.Insert(bug);
            await _uow.SaveChangesAsync();
            return bug.Id;
        }
    }
}