using System;
using DeveloperManagement.Core.Application.Interfaces;

namespace DeveloperManagement.SprintManagement.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime UtcNow => DateTime.UtcNow;
    }
}