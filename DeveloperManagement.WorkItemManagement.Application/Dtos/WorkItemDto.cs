using System;
using System.Collections.Generic;

namespace DeveloperManagement.WorkItemManagement.Application.Dtos
{
    public class WorkItemDto
    {
        public string Title { get; set; }
        public Guid? AssignedTo { get; set; }
        public int? StateId { get; set; }
        public int? StateReasonId { get; set; }
        public Guid AreaId { get; set; }
        public Guid? IterationId { get; set; }
        public string Description { get; set; }
        public int PriorityId { get; set; }
        public string RepoLink { get; set; }
        public IEnumerable<string> Comments { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public IEnumerable<AttachmentDto> Attachments { get; set; }
        public IEnumerable<RelatedWorkDto> RelatedWorks { get; set; }
    }
}