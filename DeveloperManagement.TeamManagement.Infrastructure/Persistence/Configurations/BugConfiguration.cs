namespace DeveloperManagement.TeamManagement.Infrastructure.Persistence.Configurations
{
    public class BugConfiguration : IEntityTypeConfiguration<Bug>
    {
        public void Configure(EntityTypeBuilder<Bug> builder)
        {
            builder.OwnsOne(b => b.Effort);
            builder.Property(b => b.Severity).HasColumnName("Severity_PriorityId").IsRequired();
            builder.Property(b => b.IntegratedInBuild).HasMaxLength(150);
            builder.Property(b => b.FoundInBuild).HasMaxLength(150);
            builder.Property(b => b.SystemInfo).HasMaxLength(400);
        }
    }
}