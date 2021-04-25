namespace DeveloperManagement.TeamManagement.Infrastructure.Persistence.Configurations
{
    public class FeatureConfiguration : IEntityTypeConfiguration<Feature>
    {
        public void Configure(EntityTypeBuilder<Feature> builder)
        {
            builder.Property(e => e.Risk).HasColumnName("Risk_PriorityId");
            builder.Property(e => e.ValueArea).HasColumnName("ValueAreaId");
        }
    }
}