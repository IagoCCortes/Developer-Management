using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Domain.Common.Interfaces;

namespace DeveloperManagement.WorkItemManagement.Domain.AggregateRoots.BugAggregate
{
    public interface IBugRepository : IRepository<Bug>
    {
        void ModifyPlanning(Bug bug);
        void SpecifyInfo(Bug bug);
        void ModifyEffort(Bug bug);
        void ChangeState(Bug bug);
    }
}