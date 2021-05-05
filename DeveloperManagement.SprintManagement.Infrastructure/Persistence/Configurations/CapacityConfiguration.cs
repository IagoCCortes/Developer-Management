using System;
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

            builder.Property<int>("_activityId").UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ActivityId").IsRequired();

            builder.HasOne(b => b.Activity).WithMany().IsRequired().HasForeignKey("_activityId");
            builder.Property(b => b.CapacityPerDay).IsRequired();
            var daysOffConf = builder.OwnsMany(b => b.DaysOff);
            builder.Navigation(b => b.DaysOff).Metadata.SetField("_daysOff");
            builder.Navigation(b => b.DaysOff).UsePropertyAccessMode(PropertyAccessMode.Field);
            daysOffConf.Property(b => b.InitialDateTime).IsRequired();
            daysOffConf.Property(b => b.FinalDateTime).IsRequired();


            builder.Property<Guid>("SprintId").IsRequired();

            builder.ConfigureEntityProperties();
        }
    }
}