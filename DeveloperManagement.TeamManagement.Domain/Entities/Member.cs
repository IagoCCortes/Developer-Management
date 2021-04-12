using System.Collections.Generic;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.TeamManagement.Domain.ValueObjects;

namespace DeveloperManagement.TeamManagement.Domain.Entities
{
    public class Member : Entity
    {
        public string Name { get; private set; }
        public EmailAddress EmailAddress { get; private set; }

        public Member(string name, EmailAddress emailAddress)
        {
            var errors = new Dictionary<string, string[]>();
            if (string.IsNullOrWhiteSpace(name) || emailAddress == null)
                errors.Add(nameof(Name), new[] {$"{nameof(Name)} must not be empty"});
            if (emailAddress == null)
                errors.Add(nameof(EmailAddress), new[] {$"{nameof(EmailAddress)} must not be empty"});

            if (errors.Any())
                throw new DomainException(errors);
            
            Name = name;
            EmailAddress = emailAddress;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(nameof(Name), $"{nameof(Name)} must not be empty");

            Name = name;
        }

        public void ChangeEmailAddress(EmailAddress email)
            => EmailAddress = email ??
                              throw new DomainException(nameof(EmailAddress),
                                  $"{nameof(EmailAddress)} must not be empty");
    }
}