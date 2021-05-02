using System;
using DeveloperManagement.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.SprintManagement.Infrastructure.Persistence.Configurations
{
    public static class ConfigurationExtensions
    {
        public static EntityTypeBuilder<T> ConfigureEntityProperties<T>(this EntityTypeBuilder<T> builder) where T : Entity
        {
            builder.HasKey(e => e.Id);
            builder.Property<DateTime>("Created").IsRequired();
            builder.Property<string>("CreatedBy").HasMaxLength(150).IsRequired();
            builder.Property<DateTime>("LastModified");
            builder.Property<string>("LastModifiedBy").HasMaxLength(150);
            return builder;
        }
    }
}