using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Configurations
{
    public class IssueConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.ConfigureWorkItemChildren();
            builder.ConfigureEntityProperties();
        }
    }
}