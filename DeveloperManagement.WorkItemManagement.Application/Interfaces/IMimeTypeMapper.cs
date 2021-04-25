namespace DeveloperManagement.WorkItemManagement.Application.Interfaces
{
    public interface IMimeTypeMapper
    {
        bool TryGetMimeType(string str, out string mimeType);
        string GetMimeType(string str);
        string GetExtension(string mimeType, bool throwErrorIfNotFound = true);
    }
}