using System;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.Ignore(e => e.DomainEvents);
            builder.OwnsOne(t => t.Effort);
            builder.Property(t => t.Activity).HasColumnName("ActivityId").IsRequired();
            builder.Property(t => t.IntegratedInBuild).HasMaxLength(150);
            
            builder.ConfigureWorkItemChildren();
            builder.ConfigureEntityProperties();
        }
    }
}