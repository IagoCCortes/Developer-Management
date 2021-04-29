using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories
{
    public class BugRepository : GenericAggregateRepository<Bug>, IBugRepository
    {
        private readonly IDapperConnectionFactory _connectionFactory;

        public BugRepository(IDapperConnectionFactory connectionFactory, List<(string sql, DatabaseEntity dbEntity, OperationType operationType)> changes) :
            base(changes)
        {
            _connectionFactory = connectionFactory;
        }

        public override async Task<Bug> GetByIdAsync(Guid id)
        {
            var sql = 
                // WorkItem
                "SELECT Id, Title, AssignedTo, StateId, AreaId, IterationId, Description, " +
                "PriorityId, RepoLink, StateReasonId " + 
                "FROM WorkItem Where Id = @Id;" +
                // Bug
                "SELECT EffortOriginalEstimate, EffortRemaining, EffortCompleted, IntegratedInBuild, " +
                "StoryPoints, SeverityId, ActivityId, SystemInfo, FoundInBuild " +
                "FROM Bug WHERE Id = @Id;" +
                // RelatedWorks
                "SELECT Id, LinkTypeId, Comment, Url, ReferencedWorkItemId, WorkItemId " +
                "FROM RelatedWork WHERE WorkItemId = @Id;" +
                // Comments
                "SELECT Text FROM Comment WHERE WorkItemId = @Id;" +
                // Tags
                "SELECT Text FROM Tag WHERE WorkItemId = @Id;" + 
                // Attachments
                "SELECT Path, FileName, MimeType, Created FROM Attachment WHERE WorkItemId = @Id;";
            
            using var connection = await _connectionFactory.CreateConnectionAsync();
            using var multi = await connection.QueryMultipleAsync(sql, new {Id = id});
            var workItemDao = multi.Read<WorkItemDao>().FirstOrDefault();
            var bugDao = multi.Read<BugDao>().FirstOrDefault();
            var relatedWorkDaos = multi.Read<RelatedWorkDao>();
            var commentDaos = multi.Read<CommentDao>();
            var tagDaos = multi.Read<TagDao>();
            var attachmentDaos = multi.Read<AttachmentDao>();
            var bug = bugDao.ToBug(workItemDao, tagDaos, commentDaos, attachmentDaos, relatedWorkDaos);
            
            return bug;
        }

        public override void Insert(Bug bug)
        {
            var workItemDao = new WorkItemDao(bug);
            Changes.Add((workItemDao.BuildInsertStatement(), workItemDao, OperationType.INSERT));

            var bugDao = new BugDao((Bug) bug);
            Changes.Add((bugDao.BuildInsertStatement(), bugDao, OperationType.INSERT));

            foreach (var comment in bug.Comments)
            {
                var commentDao = new CommentDao {Text = comment.Text, WorkItemId = bug.Id};
                Changes.Add((commentDao.BuildInsertStatement(), commentDao, OperationType.INSERT));
            }

            using (var enumerator = bug.Attachments.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var attachmentDao = new AttachmentDao
                    {
                        Path = enumerator.Current.Path, FileName = enumerator.Current.FileName,
                        MimeType = enumerator.Current.MimeType, WorkItemId = bug.Id
                    };
                    Changes.Add((attachmentDao.BuildInsertStatement(), attachmentDao, OperationType.INSERT));
                }
            }

            foreach (var tag in bug.Tags)
            {
                var tagDao = new TagDao {Text = tag.Text, WorkItemId = bug.Id};
                Changes.Add((tagDao.BuildInsertStatement(), tagDao, OperationType.INSERT));
            }

            foreach (var relatedWork in bug.RelatedWorks)
            {
                var relatedWorkDao = new RelatedWorkDao(relatedWork, bugDao.Id);
                Changes.Add((relatedWorkDao.BuildInsertStatement(), relatedWorkDao, OperationType.INSERT));
            }
        }

        public async Task<int> ModifyPlanning()
        {
            throw new NotImplementedException();
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}