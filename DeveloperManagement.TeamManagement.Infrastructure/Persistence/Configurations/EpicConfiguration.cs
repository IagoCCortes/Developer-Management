namespace DeveloperManagement.TeamManagement.Infrastructure.Persistence.Configurations
{
    public class EpicConfiguration : IEntityTypeConfiguration<Epic>
    {
        public void Configure(EntityTypeBuilder<Epic> builder)
        {
            builder.Property(e => e.Risk).HasColumnName("Risk_PriorityId");
            builder.Property(e => e.ValueArea).HasColumnName("ValueAreaId");
        }
    }
}