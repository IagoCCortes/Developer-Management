using System;
using DeveloperManagement.Core.Domain.Helper;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using DeveloperManagement.WorkItemManagement.Domain.Enums;
using DeveloperManagement.WorkItemManagement.Domain.ValueObjects;
using FluentValidation;

namespace DeveloperManagement.WorkItemManagement.Application.Dtos
{
    public class AttachmentDto
    {
        public string Path { get; set; }
        public string FileName { get; set; }

        public Attachment ToAttachment(string mimeType, DateTime dateTime)
            => new Attachment(Path, FileName, mimeType, dateTime);
    }
    
    public class AttachmentDtoValidations : AbstractValidator<AttachmentDto>
    {
        public AttachmentDtoValidations Path()
        {
            RuleFor(r => r.Path).Must(p => !String.IsNullOrWhiteSpace(p))
                .WithMessage("Path must not be empty");

            return this;
        }

        public AttachmentDtoValidations Filename()
        {
            RuleFor(r => r.FileName).Must(f => !String.IsNullOrWhiteSpace(f))
                .WithMessage("File name must not be empty").Must(f => f.TryGetExtension(out var _))
                .WithMessage("File name does not contain an extension");
            return this;
        }

        public static AttachmentDtoValidations Validate() => new AttachmentDtoValidations();
    }
}