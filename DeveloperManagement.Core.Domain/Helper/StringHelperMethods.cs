using System.Collections.Generic;
using System.Linq;

namespace DeveloperManagement.Core.Domain.Helper
{
    public static class StringHelperMethods
    {
        public static List<string> AreNullOrWhiteSpace(params string[] values)
            => values.Where(value => string.IsNullOrWhiteSpace(value)).ToList();
    }
}