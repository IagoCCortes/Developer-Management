using System;
using System.Collections.Generic;

namespace DeveloperManagement.Core.Domain
{
    public class DomainException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public DomainException()
            : base("One or more Domain invariants were violated")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public DomainException(string field, string message) : this()
        {
            Errors.Add(field, new[] {message});
        }

        public DomainException(IDictionary<string, string[]> errors) : this()
        {
            Errors = errors;
        }
    }
}