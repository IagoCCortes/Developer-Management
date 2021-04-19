using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Configurations
{
    public class WorkItemConfiguration : IEntityTypeConfiguration<WorkItem>
    {
        public void Configure(EntityTypeBuilder<WorkItem> builder)
        {
            builder.Ignore(e => e.DomainEvents);

            var repoLinkConfiguration = builder.OwnsOne(w => w.RepoLink);
            repoLinkConfiguration.Property(r => r.Hyperlink).IsRequired();

            var attachmentsConfiguration = builder.OwnsMany(w => w.Attachments);
            builder.Navigation(w => w.Attachments).Metadata.SetField("_attachments");
            builder.Navigation(w => w.Attachments).UsePropertyAccessMode(PropertyAccessMode.Field);
            attachmentsConfiguration.Property(a => a.Path).HasMaxLength(1000).IsRequired();
            attachmentsConfiguration.Property(a => a.FileName).HasMaxLength(150).IsRequired();
            attachmentsConfiguration.Property(a => a.MimeType).HasMaxLength(100).IsRequired();
            attachmentsConfiguration.Property(a => a.Created).IsRequired();

            var tagsConfiguration = builder.OwnsMany(w => w.Tags);
            builder.Navigation(w => w.Tags).Metadata.SetField("_tags");
            builder.Navigation(w => w.Tags).UsePropertyAccessMode(PropertyAccessMode.Field);
            tagsConfiguration.Property(t => t.Text).HasMaxLength(100).IsRequired();

            var commentsConfiguration = builder.OwnsMany(w => w.Comments);
            builder.Navigation(w => w.Comments).Metadata.SetField("_comments");
            builder.Navigation(w => w.Comments).UsePropertyAccessMode(PropertyAccessMode.Field);
            commentsConfiguration.Property(c => c.Text).IsRequired();
            commentsConfiguration.Property(c => c.CommentedAt).IsRequired();

            builder.Property(w => w.Title).HasMaxLength(150).IsRequired();
            builder.Property(w => w.Area).HasColumnName("AreaId").IsRequired();
            builder.Property(w => w.AssignedTo).HasColumnName("AssignedToId");
            builder.Property(w => w.Iteration).HasColumnName("IterationId");
            builder.Property(w => w.State)
                .HasColumnName("StateId")
                .IsRequired();
            builder.Property(w => w.StateReason)
                .HasColumnName("StateReasonId")
                .IsRequired();
            builder.Property(w => w.Priority)
                .HasColumnName("PriorityId")
                .IsRequired();

            builder.ConfigureEntityProperties();
        }
    }
}