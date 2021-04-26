using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DeveloperManagement.Core.Domain.Helper
{
    public static class StringHelperMethods
    {
        private static string _extensionPattern = @".(\w+)$";
        private static Regex _rgx = new Regex(_extensionPattern, RegexOptions.IgnoreCase);
        
        public static List<string> AreNullOrWhiteSpace(params (string field, string value)[] values)
            => values.Where(param => string.IsNullOrWhiteSpace(param.value)).Select(param => param.field).ToList();

        public static bool IsStringAUrl(this string source)
            => Uri.TryCreate(source, UriKind.Absolute, out var uriResult) &&
               (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

        public static bool TryGetExtension(this string fileName, out string extension)
        {
            var matches = _rgx.Matches(fileName);
            if (matches.Any())
            {
                extension = matches[0].Value;
                return true;
            }

            extension = "";
            return false;
        }
    }
}