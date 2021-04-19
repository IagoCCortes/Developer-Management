using System;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Configurations
{
    public static class ConfigurationExtensions
    {
        public static EntityTypeBuilder<T> ConfigureEntityProperties<T>(this EntityTypeBuilder<T> builder) where T : Entity
        {
            builder.HasKey(e => e.Id);
            builder.Property<DateTime>("Created").IsRequired();
            builder.Property<string>("CreatedBy").IsRequired();
            builder.Property<DateTime>("LastModified");
            builder.Property<string>("LastModifiedBy");
            return builder;
        }
        
        public static EntityTypeBuilder<T> ConfigureWorkItemChildren<T>(this EntityTypeBuilder<T> builder) where T : WorkItem
        {
            builder.Ignore(e => e.Comments);
            builder.Ignore(e => e.Tags);
            builder.Ignore(e => e.Attachments);
            builder.Ignore(e => e.RelatedWorks);
            builder.Ignore(e => e.Title);
            builder.Ignore(e => e.AssignedTo);
            builder.Ignore(e => e.Description);
            builder.Ignore(e => e.State);
            builder.Ignore(e => e.StateReason);
            builder.Ignore(e => e.Area);
            builder.Ignore(e => e.Iteration);
            builder.Ignore(e => e.Priority);
            builder.Ignore(e => e.RepoLink);
            return builder;
        }
    }
}