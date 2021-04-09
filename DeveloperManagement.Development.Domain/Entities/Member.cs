using DeveloperManagement.Core.Domain;
using DeveloperManagement.Development.Domain.Enums;
using DeveloperManagement.Development.Domain.ValueObjects;

namespace DeveloperManagement.Development.Domain.Entities
{
    public class Member : Entity
    {
        public string Name { get; private set; }
        public EmailAddress EmailAddress { get; private set; }

        public Member(string name, EmailAddress emailAddress)
        {
            if (string.IsNullOrWhiteSpace(name) || emailAddress == null)
                throw new DomainException(nameof(Name), $"{nameof(Name)} must not be empty");
            
            Name = name;
            EmailAddress = emailAddress;
        }

        public void ChangeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new DomainException(nameof(Name), $"{nameof(Name)} must not be empty");
        }
    }
}