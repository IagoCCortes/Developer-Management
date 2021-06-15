using System;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate.Events;
using DeveloperManagement.WorkItemManagement.Domain.Common.Entities;
using DeveloperManagement.WorkItemManagement.Domain.Common.Enums;
using DeveloperManagement.WorkItemManagement.Domain.Common.ValueObjects;
using FluentAssertions;
using Xunit;

namespace WorkItemManagement.UnitTests.Domain.BugAggregate
{
    public class BugTests
    {
        [Fact]
        public void BugBuild_GivenValidParameters_ShouldAddBugCreatedEvent()
        {
            //Arrange    
            var bug = CreateBug();

            //Act - Assert
            bug.DomainEvents.Should().Contain(de => de is BugCreatedEvent);
        }

        [Fact]
        public void BugBuild_GivenNullEffort_ShouldFail()
        {
            //Arrange  
            Action act = () => new Bug.BugBuilder("test", Guid.NewGuid(), null);

            //Act - Assert
            act.Should().Throw<DomainException>()
                .WithMessage("One or more Domain invariants were violated")
                .Where(de => de.Errors[nameof(Effort)].Contains("Effort cannot be empty"));
        }

        [Fact]
        public void BugBuild_GivenInvalidState_ShouldFail()
        {
            //Arrange  
            Action act = () => new Bug.BugBuilder("test", Guid.NewGuid(), 
                new Effort(10, 10, 10), Priority.Critical,
                StateReason.Completed, Priority.Low);

            //Act - Assert
            act.Should().Throw<DomainException>()
                .WithMessage("One or more Domain invariants were violated")
                .Where(de => de.Errors[nameof(StateReason)].Contains("Invalid reason for current state"));
        }

        [Fact]
        public void ModifyPlanning_GivenValidParameters_ShouldAddBugPlanningModifiedEvent()
        {
            //Arrange    
            var storyPoints = 20;
            var priority = Priority.High;
            var severity = Priority.Low;
            var activity = Activity.Documentation;
            var bug = CreateBug();

            //Act
            bug.ModifyPlanning(storyPoints, priority, severity, activity);

            //Assert
            bug.DomainEvents.Should().Contain(de => de is BugPlanningModifiedEvent);
        }

        private Bug CreateBug()
        {
            var title = "test";
            var teamId = Guid.NewGuid();
            var effort = new Effort(10, 10, 10);

            var integratedInBuild = "test";
            var storyPoints = 10;
            var systemInfo = "test";
            var foundInBuild = "test";

            var description = "test";
            var assignedTo = Guid.NewGuid();
            var sprintId = Guid.NewGuid();
            var repoLink = new Link("http://www.test.com");

            var tag = new Tag("test");

            var path = "C\\test";
            var fileName = "test";
            var mimeType = "application/pdf";
            var created = new DateTime(2021, 6, 7);
            var attachment = new Attachment(path, fileName, mimeType, created);

            var gitHubLink = new Link("https://github.com/IagoCCortes");
            var comment = "test";
            var relatedWork = RelatedWork.CreateGitHubRelatedWork(gitHubLink, comment);

            return new Bug.BugBuilder(title, teamId, effort)
                .SetBugOptionalFields(integratedInBuild, storyPoints, systemInfo, foundInBuild)
                .SetWorkItemOptionalFields(description, assignedTo, sprintId, repoLink)
                .AddTag(tag)
                .AddAttachment(attachment)
                .AddRelatedWork(relatedWork)
                .BuildWorkItem();
        }
    }
}