using System;
using System.Collections.Generic;
using DeveloperManagement.WorkItemManagement.Domain.Entities.WorkItems;
using DeveloperManagement.WorkItemManagement.Domain.Interfaces;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Daos;
using DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Helper;

namespace DeveloperManagement.WorkItemManagement.Infrastructure.Persistence.Repositories
{
    public class WorkItemRepository : GenericWriteRepository<WorkItem>, IWorkItemRepository
    {
        public WorkItemRepository(List<(string sql, DatabaseEntity dbEntity, OperationType operationType)> changes) :
            base(changes)
        {
        }

        public override void Insert(WorkItem workItem)
        {
            var workItemDao = new WorkItemDao(workItem);
            Changes.Add((workItemDao.BuildInsertStatement(), workItemDao, OperationType.INSERT));

            var bugDao = new BugDao((Bug) workItem);
            Changes.Add((bugDao.BuildInsertStatement(), bugDao, OperationType.INSERT));

            foreach (var comment in workItem.Comments)
            {
                var commentDao = new CommentDao {Text = comment.Text, WorkItemId = workItem.Id};
                Changes.Add((commentDao.BuildInsertStatement(), commentDao, OperationType.INSERT));
            }

            using (var enumerator = workItem.Attachments.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    var attachmentDao = new AttachmentDao
                    {
                        Path = enumerator.Current.Path, FileName = enumerator.Current.FileName,
                        MimeType = enumerator.Current.MimeType, WorkItemId = workItem.Id
                    };
                    Changes.Add((attachmentDao.BuildInsertStatement(), attachmentDao, OperationType.INSERT));
                }
            }

            foreach (var tag in workItem.Tags)
            {
                var tagDao = new TagDao {Text = tag.Text, WorkItemId = workItem.Id};
                Changes.Add((tagDao.BuildInsertStatement(), tagDao, OperationType.INSERT));
            }

            foreach (var relatedWork in workItem.RelatedWorks)
            {
                var relatedWorkDao = new RelatedWorkDao(relatedWork, bugDao.Id);
                Changes.Add((relatedWorkDao.BuildInsertStatement(), relatedWorkDao, OperationType.INSERT));
            }
        }

        public override void Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}