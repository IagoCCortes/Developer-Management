using System;
using System.Collections.Generic;
using System.Linq;

namespace DeveloperManagement.Core.Domain.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsOneOf<T>(this T self, params T[] values)
            => values.Contains(self);

        public static bool HasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
            => !props(self).Any();

        public static bool HasSome<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
            => props(self).Any();
    }
}