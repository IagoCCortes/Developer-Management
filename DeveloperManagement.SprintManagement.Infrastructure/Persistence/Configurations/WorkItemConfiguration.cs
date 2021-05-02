using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.SprintManagement.Infrastructure.Persistence.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.ToTable("WorkItem");
            
            builder.OwnsOne(b => b.Effort, a =>
            {
                a.Property(e => e.Completed).HasColumnName("Completed");
                a.Property(e => e.Remaining).HasColumnName("Remaining");
                a.Property(e => e.OriginalEstimate).HasColumnName("OriginalEstimate");
            });
            builder.Navigation(b => b.Effort).IsRequired();

            builder.ConfigureEntityProperties();
        }
    }
}