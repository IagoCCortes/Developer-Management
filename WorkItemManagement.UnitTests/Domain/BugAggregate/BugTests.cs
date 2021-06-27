using System;
using System.Linq;
using DeveloperManagement.Core.Domain;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BaseWorkItemAggregate.Events;
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
        public void BugBuild_GivenValidParameters_ShouldAddDomainEvent()
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
        public void ModifyPlanning_GivenValidParameters_ShouldModifyPlanningAndAddDomainEvent()
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
            bug.StoryPoints.Should().Be(storyPoints);
            bug.Priority.Should().Be(priority);
            bug.Severity.Should().Be(severity);
            bug.Activity.Should().Be(activity);
        }
        
        [Fact]
        public void SpecifyBugInfo_GivenValidParameters_ShouldAddBugInfoAndAddDomainEvent()
        {
            //Arrange    
            var description = "Test";
            var systemInfo = "Test";
            var foundInBuild = "Test";
            var integratedInBuild = "Test";
            var bug = CreateBug();

            //Act
            bug.SpecifyBugInfo(description, systemInfo, foundInBuild, integratedInBuild);

            //Assert
            bug.DomainEvents.Should().Contain(de => de is BugInfoModifiedEvent);
            bug.Description.Should().Be(description);
            bug.SystemInfo.Should().Be(systemInfo);
            bug.FoundInBuild.Should().Be(foundInBuild);
            bug.IntegratedInBuild.Should().Be(integratedInBuild);
        }
        
        [Fact]
        public void ModifyEffort_GivenValidParameters_ShouldModifyEffortAndAddDomainEvent()
        {
            //Arrange    
            var remaining = 10;
            var completed = 5;
            var bug = CreateBug();

            //Act
            bug.ModifyEffort(remaining, completed);

            //Assert
            bug.DomainEvents.Should().Contain(de => de is BugEffortModifiedEvent);
            bug.Effort.Remaining.Should().Be(remaining);
            bug.Effort.Completed.Should().Be(completed);
        }
        
        [Theory]
        [InlineData(WorkItemState.New, StateReason.New)]
        [InlineData(WorkItemState.New, StateReason.BuildFailure)]
        [InlineData(WorkItemState.Active, StateReason.Approved)]
        [InlineData(WorkItemState.Active, StateReason.Investigate)]
        [InlineData(WorkItemState.Resolved, StateReason.Fixed)]
        [InlineData(WorkItemState.Resolved, StateReason.AsDesigned)]
        [InlineData(WorkItemState.Resolved, StateReason.CannotReproduce)]
        [InlineData(WorkItemState.Resolved, StateReason.CopiedToBacklog)]
        [InlineData(WorkItemState.Resolved, StateReason.Deferred)]
        [InlineData(WorkItemState.Resolved, StateReason.Duplicate)]
        [InlineData(WorkItemState.Resolved, StateReason.Obsolete)]
        [InlineData(WorkItemState.Closed, StateReason.FixedAndVerified)]
        [InlineData(WorkItemState.Closed, StateReason.AsDesigned)]
        [InlineData(WorkItemState.Closed, StateReason.CannotReproduce)]
        [InlineData(WorkItemState.Closed, StateReason.CopiedToBacklog)]
        [InlineData(WorkItemState.Closed, StateReason.Deferred)]
        [InlineData(WorkItemState.Closed, StateReason.Duplicate)]
        [InlineData(WorkItemState.Closed, StateReason.Obsolete)]
        public void ModifyState_GivenValidParameters_ShouldModifyStateAndAddDomainEvent(WorkItemState state, StateReason stateReason)
        {
            //Arrange
            var bug = CreateBug();

            //Act
            bug.ModifyState(state, stateReason);

            //Assert
            bug.DomainEvents.Should().Contain(de => de is WorkItemStateModified);
            bug.State.Should().Be(state);
            bug.StateReason.Should().Be(stateReason);
        }
        
        [Fact]
        public void ModifyState_InvalidBugState_ShouldThrowException()
        {
            //Arrange
            var state = WorkItemState.Removed;
            var stateReason = StateReason.Approved;
            var bug = CreateBug();
            Action action = () => bug.ModifyState(state, stateReason); 

            //Act - Assert
            action.Should().Throw<DomainException>()
                .WithMessage("One or more Domain invariants were violated")
                .Where(de => de.Errors[nameof(bug.State)].Contains($"A {nameof(Bug)} does not have a {state} state"));
        }
        
        [Fact]
        public void ModifyState_InvalidStateAndReasonCombination_ShouldThrowException()
        {
            //Arrange
            var state = WorkItemState.New;
            var stateReason = StateReason.Approved;
            var bug = CreateBug();
            Action action = () => bug.ModifyState(state, stateReason); 

            //Act - Assert
            action.Should().Throw<DomainException>()
                .WithMessage("One or more Domain invariants were violated")
                .Where(de => de.Errors[nameof(bug.StateReason)].Contains("Invalid reason for current state"));
        }
        
        [Fact]
        public void ModifyState_ClosedState_ShouldUpdateEffort()
        {
            //Arrange
            var state = WorkItemState.Closed;
            var stateReason = StateReason.FixedAndVerified;
            var bug = CreateBug();
            var expectedCompleted = bug.Effort.Completed + bug.Effort.Remaining;
            
            //Act
            bug.ModifyState(state, stateReason);

            //Assert
            bug.Effort.Remaining.Should().Be(0);
            bug.Effort.Completed.Should().Be(expectedCompleted);
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