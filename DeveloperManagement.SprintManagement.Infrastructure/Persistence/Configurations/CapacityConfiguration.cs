using DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DeveloperManagement.SprintManagement.Infrastructure.Persistence.Configurations
{
    public class CapacityConfiguration : IEntityTypeConfiguration<Capacity>
    {
        public void Configure(EntityTypeBuilder<Capacity> builder)
        {
            builder.ToTable("Capacity");
            builder.Property(b => b.Activity).HasColumnName("ActivityId").IsRequired();
            builder.Property(b => b.CapacityPerDay).IsRequired();
            var daysOffConf = builder.OwnsMany(b => b.DaysOff);
            builder.Navigation(b => b.DaysOff).Metadata.SetField("_daysOff");
            builder.Navigation(b => b.DaysOff).UsePropertyAccessMode(PropertyAccessMode.Field);
            daysOffConf.Property(b => b.InitialDateTime).IsRequired();
            daysOffConf.Property(b => b.FinalDateTime).IsRequired();

            builder.ConfigureEntityProperties();
        }
    }
}