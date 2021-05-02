using DeveloperManagement.Core.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace DeveloperManagement.SprintManagement.WebApi.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserId = httpContextAccessor.HttpContext?.Request?.Headers["user"];
        }

        public string UserId { get; set; }
    }
}