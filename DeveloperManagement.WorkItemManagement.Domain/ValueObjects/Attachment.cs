using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using DeveloperManagement.Core.Domain;
using static DeveloperManagement.Core.Domain.Helper.StringHelperMethods;

namespace DeveloperManagement.WorkItemManagement.Domain.ValueObjects
{
    public class Attachment : ValueObject
    {
        public string Path { get; private set; }
        public string FileName { get; private set; }
        public string MimeType { get; private set; }
        public DateTime Created { get; private set; }

        public Attachment(string path, string fileName, string mimeType, DateTime created)
        {
            var invalidFields = AreNullOrWhiteSpace((nameof(Path), path), (nameof(FileName), fileName),
                (nameof(MimeType), mimeType));
            if (invalidFields.Any())
                throw new DomainException(invalidFields
                    .Select(x => new {Key = nameof(x), Value = new[] {$"{nameof(x)} cannot be empty"}})
                    .ToDictionary(x => x.Key, x => x.Value));

            Path = path;
            FileName = fileName;
            MimeType = mimeType;
            Created = created;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Path;
            yield return MimeType;
            yield return Created;
        }
    }
}