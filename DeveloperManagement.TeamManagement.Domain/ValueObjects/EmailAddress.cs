using System.Collections.Generic;
using System.Linq;
using DeveloperManagement.Core.Domain;
using static DeveloperManagement.Core.Domain.Helper.StringHelperMethods;

namespace DeveloperManagement.TeamManagement.Domain.ValueObjects
{
    public class EmailAddress : ValueObject
    {
        public string Username { get; }
        public string MailServer { get; }
        public string TopLevelDomain { get; }

        public EmailAddress(string username, string mailServer, string topLevelDomain)
        {
            var invalidFields = AreNullOrWhiteSpace(username, mailServer, topLevelDomain);
            if (invalidFields.Any())
                throw new DomainException(invalidFields
                    .Select(x => new {Key = nameof(x), Value = new[] {$"{nameof(x)} cannot be empty"}})
                    .ToDictionary(x => x.Key, x => x.Value));

            Username = username;
            MailServer = mailServer;
            TopLevelDomain = topLevelDomain;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Username;
            yield return MailServer;
            yield return TopLevelDomain;
        }

        public override string ToString()
            => $"{Username}@{MailServer}{TopLevelDomain}";
    }
}