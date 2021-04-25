using System;

namespace DeveloperManagement.TeamManagement.Infrastructure.Persistence.Configurations
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
    }
}