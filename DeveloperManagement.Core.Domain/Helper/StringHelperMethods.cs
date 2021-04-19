using System.Collections.Generic;
using System.Linq;

namespace DeveloperManagement.Core.Domain.Helper
{
    public static class StringHelperMethods
    {
        public static List<string> AreNullOrWhiteSpace(params (string field, string value)[] values)
            => values.Where(param => string.IsNullOrWhiteSpace(param.value)).Select(param => param.field).ToList();
    }
}