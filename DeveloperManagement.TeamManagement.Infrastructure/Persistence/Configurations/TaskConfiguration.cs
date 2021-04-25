namespace DeveloperManagement.TeamManagement.Infrastructure.Persistence.Configurations
{
    public class TaskConfiguration : IEntityTypeConfiguration<Task>
    {
        public void Configure(EntityTypeBuilder<Task> builder)
        {
            builder.OwnsOne(t => t.Effort);
            builder.Property(t => t.Activity).HasColumnName("ActivityId").IsRequired();
            builder.Property(t => t.IntegratedInBuild).HasMaxLength(150);
        }
    }
}