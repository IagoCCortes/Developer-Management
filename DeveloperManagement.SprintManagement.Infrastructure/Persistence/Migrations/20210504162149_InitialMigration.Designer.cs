﻿// <auto-generated />
using System;
using DeveloperManagement.SprintManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeveloperManagement.SprintManagement.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210504162149_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Activity", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Capacity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("CapacityPerDay")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("SprintId")
                        .HasColumnType("char(36)");

                    b.Property<int>("_activityId")
                        .HasColumnType("int")
                        .HasColumnName("ActivityId");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.HasIndex("_activityId");

                    b.ToTable("Capacity");
                });

            modelBuilder.Entity("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Sprint", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("varchar(250)");

                    b.Property<Guid>("TeamId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.ToTable("Sprint");
                });

            modelBuilder.Entity("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.WorkItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150)");

                    b.Property<Guid>("SprintId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("SprintId");

                    b.ToTable("WorkItem");
                });

            modelBuilder.Entity("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Capacity", b =>
                {
                    b.HasOne("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Sprint", null)
                        .WithMany("Capacity")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("_activityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsMany("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Period", "DaysOff", b1 =>
                        {
                            b1.Property<Guid>("CapacityId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<DateTime>("FinalDateTime")
                                .HasColumnType("datetime(6)");

                            b1.Property<DateTime>("InitialDateTime")
                                .HasColumnType("datetime(6)");

                            b1.HasKey("CapacityId", "Id");

                            b1.ToTable("Capacity_DaysOff");

                            b1.WithOwner()
                                .HasForeignKey("CapacityId");
                        });

                    b.Navigation("Activity");

                    b.Navigation("DaysOff");
                });

            modelBuilder.Entity("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Sprint", b =>
                {
                    b.OwnsOne("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Period", "Period", b1 =>
                        {
                            b1.Property<Guid>("SprintId")
                                .HasColumnType("char(36)");

                            b1.Property<DateTime>("FinalDateTime")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("FinalDate");

                            b1.Property<DateTime>("InitialDateTime")
                                .HasColumnType("datetime(6)")
                                .HasColumnName("InitialDate");

                            b1.HasKey("SprintId");

                            b1.ToTable("Sprint");

                            b1.WithOwner()
                                .HasForeignKey("SprintId");
                        });

                    b.OwnsOne("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.WorkLoad", "WorkLoad", b1 =>
                        {
                            b1.Property<Guid>("SprintId")
                                .HasColumnType("char(36)");

                            b1.Property<decimal>("CompletedPercentage")
                                .HasColumnType("decimal(65,30)")
                                .HasColumnName("CompletedPercentage");

                            b1.Property<decimal>("CompletedWorkHours")
                                .HasColumnType("decimal(65,30)")
                                .HasColumnName("CompletedWorkHours");

                            b1.Property<decimal>("RemainingWorkHours")
                                .HasColumnType("decimal(65,30)")
                                .HasColumnName("RemainingWorkHours");

                            b1.Property<int>("TotalItemsOriginalEstimate")
                                .HasColumnType("int")
                                .HasColumnName("TotalItemsOriginalEstimate");

                            b1.HasKey("SprintId");

                            b1.ToTable("Sprint");

                            b1.WithOwner()
                                .HasForeignKey("SprintId");
                        });

                    b.Navigation("Period")
                        .IsRequired();

                    b.Navigation("WorkLoad")
                        .IsRequired();
                });

            modelBuilder.Entity("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.WorkItem", b =>
                {
                    b.HasOne("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Sprint", null)
                        .WithMany("WorkItems")
                        .HasForeignKey("SprintId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Effort", "Effort", b1 =>
                        {
                            b1.Property<Guid>("WorkItemId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("Completed")
                                .HasColumnType("int")
                                .HasColumnName("Completed");

                            b1.Property<int>("OriginalEstimate")
                                .HasColumnType("int")
                                .HasColumnName("OriginalEstimate");

                            b1.Property<int>("Remaining")
                                .HasColumnType("int")
                                .HasColumnName("Remaining");

                            b1.HasKey("WorkItemId");

                            b1.ToTable("WorkItem");

                            b1.WithOwner()
                                .HasForeignKey("WorkItemId");
                        });

                    b.Navigation("Effort")
                        .IsRequired();
                });

            modelBuilder.Entity("DeveloperManagement.SprintManagement.Domain.AggregateRoots.SprintAggregate.Sprint", b =>
                {
                    b.Navigation("Capacity");

                    b.Navigation("WorkItems");
                });
#pragma warning restore 612, 618
        }
    }
}
