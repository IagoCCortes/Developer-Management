namespace DeveloperManagement.TeamManagement.Infrastructure.Persistence.Configurations
{
    public class UserStoryConfiguration : IEntityTypeConfiguration<UserStory>
    {
        public void Configure(EntityTypeBuilder<UserStory> builder)
        {
            builder.Property(b => b.Risk).HasColumnName("Risk_PriorityId");
            builder.Property(t => t.ValueArea).HasColumnName("ValueAreaId").IsRequired();
        }
    }
}