using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Configurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.Property(e => e.Risk).HasColumnName("Risk_PriorityId");
            builder.Property(e => e.ValueArea).HasColumnName("ValueAreaId");
            
            builder.ConfigureWorkItemChildren();
            builder.ConfigureEntityProperties();
        }
    }
}