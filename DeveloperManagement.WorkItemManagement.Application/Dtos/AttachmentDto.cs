using System;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;

namespace DeveloperManagement.WorkItemManagement.Application.Dtos
{
    public class AttachmentDto
    {
        public string Path { get; set; }
        public string FileName { get; set; }

        public Attachment ToAttachment(string mimeType, DateTime dateTime)
            => new Attachment(Path, FileName, mimeType, dateTime);
    }
}