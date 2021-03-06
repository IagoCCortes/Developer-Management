using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories
{
    public class BugRepository : IBugRepository
    {
        private readonly IDapperConnectionFactory _connectionFactory;
        private readonly List<DatabaseOperationData> _changes;

        public BugRepository(IDapperConnectionFactory connectionFactory, List<DatabaseOperationData> changes)
        {
            _connectionFactory = connectionFactory;
            _changes = changes;
        }

        public async Task<Bug> GetByIdAsync(Guid id)
        {
            var sql =
                // WorkItem
                "SELECT Id, Title, AssignedTo, StateId, TeamId, SprintId, Description, " +
                "PriorityId, RepoLink, StateReasonId " +
                "FROM WorkItem Where Id = @Id;" +
                // Bug
                "SELECT EffortOriginalEstimate, EffortRemaining, EffortCompleted, IntegratedInBuild, " +
                "StoryPoints, SeverityId, ActivityId, SystemInfo, FoundInBuild " +
                "FROM Bug WHERE Id = @Id;" +
                // RelatedWorks
                "SELECT Id, LinkTypeId, Comment, Url, ReferencedWorkItemId, WorkItemId " +
                "FROM RelatedWork WHERE WorkItemId = @Id;" +
                // Tags
                "SELECT Text FROM Tag WHERE WorkItemId = @Id;" +
                // Attachments
                "SELECT Path, FileName, MimeType, Created FROM Attachment WHERE WorkItemId = @Id;";

            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var multi = await connection.QueryMultipleAsync(sql, new {Id = id});
            var workItemDao = multi.Read<WorkItemDao>().FirstOrDefault();

            if (workItemDao == null) return null;

            var bugDao = multi.Read<BugDao>().FirstOrDefault();
            var relatedWorkDaos = multi.Read<RelatedWorkDao>();
            var tagDaos = multi.Read<TagDao>();
            var attachmentDaos = multi.Read<AttachmentDao>();
            var bug = bugDao!.ToBug(workItemDao, tagDaos, attachmentDaos, relatedWorkDaos);

            return bug;
        }

        public void Insert(Bug bug)
        {
            var workItemDao = new WorkItemDao(bug);
            AddChange(workItemDao.BuildInsertStatement(), workItemDao, OperationType.INSERT);

            var bugDao = new BugDao(bug);
            AddChange(bugDao.BuildInsertStatement(), bugDao, OperationType.INSERT);

            using (var enumerator = bug.Attachments.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var attachmentDao = new AttachmentDao
                    {
                        Path = enumerator.Current!.Path, FileName = enumerator.Current.FileName,
                        MimeType = enumerator.Current.MimeType, WorkItemId = bug.Id
                    };
                    AddChange(attachmentDao.BuildInsertStatement(), attachmentDao, OperationType.INSERT);
                }
            }

            foreach (var tag in bug.Tags)
            {
                var tagDao = new TagDao {Text = tag.Text, WorkItemId = bug.Id};
                AddChange(tagDao.BuildInsertStatement(), tagDao, OperationType.INSERT);
            }

            foreach (var relatedWork in bug.RelatedWorks)
            {
                var relatedWorkDao = new RelatedWorkDao(relatedWork, bugDao.Id);
                AddChange(relatedWorkDao.BuildInsertStatement(), relatedWorkDao, OperationType.INSERT);
            }
        }

        public void ModifyPlanning(Bug bug)
        {
            var workItemDao = new WorkItemDao
            {
                Id = bug.Id,
                PriorityId = (int) bug.Priority,
            };
            var workItemSql = OperationsHelper.BuildUpdateStatement(workItemDao.GetTableName(), nameof(workItemDao.Id),
                nameof(workItemDao.PriorityId));
            AddChange(workItemSql, workItemDao, OperationType.UPDATE);

            var bugDao = new BugDao(bug);
            var bugSql =
                OperationsHelper.BuildUpdateStatement(bugDao.GetTableName(), nameof(bug.Id),
                    nameof(bugDao.StoryPoints), nameof(bugDao.SeverityId), nameof(bugDao.ActivityId));
            AddChange(bugSql, bugDao, OperationType.UPDATE);
        }

        public void SpecifyInfo(Bug bug)
        {
            var workItemDao = new WorkItemDao
            {
                Id = bug.Id,
                Description = bug.Description,
            };
            var workItemSql = OperationsHelper.BuildUpdateStatement(workItemDao.GetTableName(), nameof(workItemDao.Id),
                nameof(workItemDao.Description));
            AddChange(workItemSql, workItemDao, OperationType.UPDATE);

            var bugDao = new BugDao(bug);
            var bugSql =
                OperationsHelper.BuildUpdateStatement(bugDao.GetTableName(), nameof(bug.Id),
                    nameof(bugDao.IntegratedInBuild), nameof(bugDao.FoundInBuild), nameof(bugDao.SystemInfo));
            AddChange(bugSql, bugDao, OperationType.UPDATE);
        }

        public void ModifyEffort(Bug bug)
        {
            var bugDao = new BugDao(bug);
            var bugSql =
                OperationsHelper.BuildUpdateStatement(bugDao.GetTableName(), nameof(bug.Id),
                    nameof(bugDao.EffortOriginalEstimate), nameof(bugDao.EffortRemaining),
                    nameof(bugDao.EffortCompleted));
            AddChange(bugSql, bugDao, OperationType.UPDATE);
        }

        public void ChangeState(Bug bug)
        {
            var workItemDao = new WorkItemDao
            {
                Id = bug.Id,
                StateId = (int) bug.State,
                StateReasonId = (int) bug.StateReason,
            };

            workItemDao.IncludeDomainEvents(bug.DomainEvents);

            var workItemSql = OperationsHelper.BuildUpdateStatement(workItemDao.GetTableName(), nameof(workItemDao.Id),
                nameof(workItemDao.StateId), nameof(workItemDao.StateReasonId));
            AddChange(workItemSql, workItemDao, OperationType.UPDATE);
        }

        public void AddComment(Bug bug)
        {
            var workItemDao = new WorkItemDao
            {
                Id = bug.Id,
                Description = bug.Description,
            };
            var workItemSql = OperationsHelper.BuildUpdateStatement(workItemDao.GetTableName(), nameof(workItemDao.Id),
                nameof(workItemDao.Description));
            AddChange(workItemSql, workItemDao, OperationType.UPDATE);

            var bugDao = new BugDao
            {
                Id = bug.Id,
                IntegratedInBuild = bug.IntegratedInBuild,
                FoundInBuild = bug.FoundInBuild,
                SystemInfo = bug.SystemInfo
            };
            var bugSql =
                OperationsHelper.BuildUpdateStatement(bugDao.GetTableName(), nameof(bug.Id),
                    nameof(bugDao.IntegratedInBuild), nameof(bugDao.FoundInBuild), nameof(bugDao.SystemInfo));
            AddChange(bugSql, bugDao, OperationType.UPDATE);
        }

        private void AddChange(string sql, DatabaseEntity dbEntity, OperationType operationType) =>
            _changes.Add(new DatabaseOperationData(typeof(Bug), sql, dbEntity, operationType));
    }
}