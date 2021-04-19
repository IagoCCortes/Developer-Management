﻿// <auto-generated />
using System;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210419114156_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.RelatedWork", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Comment")
                        .HasMaxLength(4000)
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("LinkType")
                        .HasColumnType("int")
                        .HasColumnName("LinkTypeId");

                    b.Property<Guid?>("WorkItemId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("WorkItemId");

                    b.ToTable("RelatedWorks");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.Bug", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("FoundInBuild")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150) CHARACTER SET utf8mb4");

                    b.Property<string>("IntegratedInBuild")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Severity")
                        .HasColumnType("int")
                        .HasColumnName("Severity_PriorityId");

                    b.Property<byte?>("StoryPoints")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("SystemInfo")
                        .HasMaxLength(400)
                        .HasColumnType("varchar(400) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Bugs");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.Epic", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte?>("BusinessValue")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte?>("Effort")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("Risk")
                        .HasColumnType("int")
                        .HasColumnName("Risk_PriorityId");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("TargetDate")
                        .HasColumnType("datetime(6)");

                    b.Property<byte?>("TimeCriticality")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("ValueArea")
                        .HasColumnType("int")
                        .HasColumnName("ValueAreaId");

                    b.HasKey("Id");

                    b.ToTable("Epics");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.Feature", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte?>("BusinessValue")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<byte?>("Effort")
                        .HasColumnType("tinyint unsigned");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("Risk")
                        .HasColumnType("int")
                        .HasColumnName("Risk_PriorityId");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("TargetDate")
                        .HasColumnType("datetime(6)");

                    b.Property<byte?>("TimeCriticality")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("ValueArea")
                        .HasColumnType("int")
                        .HasColumnName("ValueAreaId");

                    b.HasKey("Id");

                    b.ToTable("Features");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.Issue", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("StackRank")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Issues");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.Task", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<int>("Activity")
                        .HasColumnType("int")
                        .HasColumnName("ActivityId");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("IntegratedInBuild")
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150) CHARACTER SET utf8mb4");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.WorkItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("Area")
                        .HasColumnType("char(36)")
                        .HasColumnName("AreaId");

                    b.Property<Guid?>("AssignedTo")
                        .HasColumnType("char(36)")
                        .HasColumnName("AssignedToId");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Description")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<Guid?>("Iteration")
                        .HasColumnType("char(36)")
                        .HasColumnName("IterationId");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("Priority")
                        .HasColumnType("int")
                        .HasColumnName("PriorityId");

                    b.Property<int>("State")
                        .HasColumnType("int")
                        .HasColumnName("StateId");

                    b.Property<int>("StateReason")
                        .HasColumnType("int")
                        .HasColumnName("StateReasonId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("varchar(150) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("WorkItem");

                    b.HasDiscriminator<string>("Discriminator").HasValue("WorkItem");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.UserStory", b =>
                {
                    b.HasBaseType("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.WorkItem");

                    b.Property<string>("AcceptanceCriteria")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("Risk")
                        .HasColumnType("int");

                    b.Property<byte?>("StoryPoints")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("ValueArea")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("UserStory");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.RelatedWork", b =>
                {
                    b.HasOne("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.WorkItem", null)
                        .WithMany("RelatedWorks")
                        .HasForeignKey("WorkItemId");

                    b.OwnsOne("DeveloperManagement.WorkItemManagement.Domain.ValueObjects.Link", "Url", b1 =>
                        {
                            b1.Property<Guid>("RelatedWorkId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Hyperlink")
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("RelatedWorkId");

                            b1.ToTable("RelatedWorks");

                            b1.WithOwner()
                                .HasForeignKey("RelatedWorkId");
                        });

                    b.Navigation("Url");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.Bug", b =>
                {
                    b.OwnsOne("DeveloperManagement.WorkItemManagement.Domain.ValueObjects.Effort", "Effort", b1 =>
                        {
                            b1.Property<Guid>("BugId")
                                .HasColumnType("char(36)");

                            b1.Property<byte>("Completed")
                                .HasColumnType("tinyint unsigned");

                            b1.Property<byte>("OriginalEstimate")
                                .HasColumnType("tinyint unsigned");

                            b1.Property<byte>("Remaining")
                                .HasColumnType("tinyint unsigned");

                            b1.HasKey("BugId");

                            b1.ToTable("Bugs");

                            b1.WithOwner()
                                .HasForeignKey("BugId");
                        });

                    b.Navigation("Effort");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.Task", b =>
                {
                    b.OwnsOne("DeveloperManagement.WorkItemManagement.Domain.ValueObjects.Effort", "Effort", b1 =>
                        {
                            b1.Property<Guid>("TaskId")
                                .HasColumnType("char(36)");

                            b1.Property<byte>("Completed")
                                .HasColumnType("tinyint unsigned");

                            b1.Property<byte>("OriginalEstimate")
                                .HasColumnType("tinyint unsigned");

                            b1.Property<byte>("Remaining")
                                .HasColumnType("tinyint unsigned");

                            b1.HasKey("TaskId");

                            b1.ToTable("Tasks");

                            b1.WithOwner()
                                .HasForeignKey("TaskId");
                        });

                    b.Navigation("Effort");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.WorkItem", b =>
                {
                    b.OwnsMany("DeveloperManagement.WorkItemManagement.Domain.ValueObjects.Attachment", "Attachments", b1 =>
                        {
                            b1.Property<Guid>("WorkItemId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<DateTime>("Created")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("FileName")
                                .IsRequired()
                                .HasMaxLength(150)
                                .HasColumnType("varchar(150) CHARACTER SET utf8mb4");

                            b1.Property<string>("MimeType")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100) CHARACTER SET utf8mb4");

                            b1.Property<string>("Path")
                                .IsRequired()
                                .HasMaxLength(1000)
                                .HasColumnType("varchar(1000) CHARACTER SET utf8mb4");

                            b1.HasKey("WorkItemId", "Id");

                            b1.ToTable("Attachment");

                            b1.WithOwner()
                                .HasForeignKey("WorkItemId");
                        });

                    b.OwnsMany("DeveloperManagement.WorkItemManagement.Domain.ValueObjects.Comment", "Comments", b1 =>
                        {
                            b1.Property<Guid>("WorkItemId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<DateTime>("CommentedAt")
                                .HasColumnType("datetime(6)");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("WorkItemId", "Id");

                            b1.ToTable("Comment");

                            b1.WithOwner()
                                .HasForeignKey("WorkItemId");
                        });

                    b.OwnsOne("DeveloperManagement.WorkItemManagement.Domain.ValueObjects.Link", "RepoLink", b1 =>
                        {
                            b1.Property<Guid>("WorkItemId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Hyperlink")
                                .IsRequired()
                                .HasColumnType("longtext CHARACTER SET utf8mb4");

                            b1.HasKey("WorkItemId");

                            b1.ToTable("WorkItem");

                            b1.WithOwner()
                                .HasForeignKey("WorkItemId");
                        });

                    b.OwnsMany("DeveloperManagement.WorkItemManagement.Domain.ValueObjects.Tag", "Tags", b1 =>
                        {
                            b1.Property<Guid>("WorkItemId")
                                .HasColumnType("char(36)");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("int");

                            b1.Property<string>("Text")
                                .IsRequired()
                                .HasMaxLength(100)
                                .HasColumnType("varchar(100) CHARACTER SET utf8mb4");

                            b1.HasKey("WorkItemId", "Id");

                            b1.ToTable("Tag");

                            b1.WithOwner()
                                .HasForeignKey("WorkItemId");
                        });

                    b.Navigation("Attachments");

                    b.Navigation("Comments");

                    b.Navigation("RepoLink");

                    b.Navigation("Tags");
                });

            modelBuilder.Entity("DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems.WorkItem", b =>
                {
                    b.Navigation("RelatedWorks");
                });
#pragma warning restore 612, 618
        }
    }
}
