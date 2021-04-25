using System.Text.RegularExpressions;
using DeveloperManagement.WorkItemManagement.Application.Interfaces;
using MimeTypes;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.MimeType
{
    public class MimeTypeMapper : IMimeTypeMapper
    {
        private static string _extensionPattern = @".(\w+)$";
        private static Regex _rgx = new Regex(_extensionPattern, RegexOptions.IgnoreCase);
        
        public bool TryGetMimeType(string str, out string mimeType)
            => MimeTypeMap.TryGetMimeType(str, out mimeType);

        public string GetMimeType(string fileName)
        {
            var extension = _rgx.Match(fileName).Value;
            return MimeTypeMap.GetMimeType(extension);
        }

        public string GetExtension(string mimeType, bool throwErrorIfNotFound = true)
            => MimeTypeMap.GetExtension(mimeType, throwErrorIfNotFound);
    }
}