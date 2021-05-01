using System;
using System.Collections.Generic;
using DeveloperManagement.Core.Domain;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.CommentAggregate
{
    public class Reaction : ValueObject
    {
        public string By { get; private set; }
        public Reactiontype Reactiontype { get; private set; }

        public Reaction(string by, Reactiontype reactiontype)
        {
            By = by;
            Reactiontype = reactiontype;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return By;
            yield return Reactiontype;
        }
    }
}