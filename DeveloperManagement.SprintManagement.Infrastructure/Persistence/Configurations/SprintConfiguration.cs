using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.SprintManagement.Infrastructure.Persistence.Configurations
{
    public class SprintConfiguration : IEntityTypeConfiguration<Sprint>
    {
        public void Configure(EntityTypeBuilder<Sprint> builder)
        {
            builder.ToTable("Sprint");
            builder.Ignore(e => e.DomainEvents);
            builder.OwnsOne(b => b.WorkLoad);
            builder.Property(b => b.Name).HasMaxLength(250).IsRequired();
            builder.OwnsOne(b => b.Period, a =>
            {
                a.Property(p => p.InitialDateTime).HasColumnName("InitialDate");
                a.Property(p => p.FinalDateTime).HasColumnName("FinalDate");
            });
            builder.OwnsOne(b => b.WorkLoad, a =>
            {
                a.Property(w => w.CompletedPercentage).HasColumnName("CompletedPercentage");
                a.Property(w => w.CompletedWorkHours).HasColumnName("CompletedWorkHours");
                a.Property(w => w.RemainingWorkHours).HasColumnName("RemainingWorkHours");
                a.Property(w => w.TotalItemsOriginalEstimate).HasColumnName("TotalItemsOriginalEstimate");
            });
            builder.Navigation(b => b.WorkLoad).IsRequired();
            builder.Navigation(b => b.Period).IsRequired();
            builder.Property(b => b.TeamId).IsRequired();
            
            var workItemNavigation = builder.Metadata.FindNavigation(nameof(Sprint.WorkItems));
            workItemNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);
            
            var capacityNavigation = builder.Metadata.FindNavigation(nameof(Sprint.Capacity));
            capacityNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.ConfigureEntityProperties();
        }
    }
}