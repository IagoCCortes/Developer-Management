using DeveloperManagement.WorkItemManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Configurations
{
    public class RelatedWorkConfiguration : IEntityTypeConfiguration<RelatedWork>
    {
        public void Configure(EntityTypeBuilder<RelatedWork> builder)
        {
            builder.ConfigureEntityProperties();
            builder.OwnsOne(r => r.Url);
            builder.Property(r => r.LinkType).HasColumnName("LinkTypeId").IsRequired();
            builder.Property(r => r.Comment).HasMaxLength(4000);
        }
    }
}