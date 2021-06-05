using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Application.Commands.ChangeState;
using DeveloperManagement.WorkItemManagement.Application.Commands.CreateBug;
using DeveloperManagement.WorkItemManagement.Application.Commands.ModifyBugPlanning;
using DeveloperManagement.WorkItemManagement.Application.Commands.ModifyEffort;
using DeveloperManagement.WorkItemManagement.Application.Commands.SpecifyBugInfo;
using DeveloperManagement.WorkItemManagement.Application.Queries.GetBugById;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperManagement.WorkItemManagement.WebApi.Controllers
{
    public class BugController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AddBug(CreateBugCommand command) => Ok(await Mediator.Send(command));
        
        [HttpPatch("planning")]
        public async Task<ActionResult> ModifyBugPlanning(ModifyBugPlanningCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        [HttpPatch("info")]
        public async Task<ActionResult> SpecifyBugInfo(SpecifyBugInfoCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        [HttpPatch("effort")]
        public async Task<ActionResult> ModifyEffort(ModifyEffortCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        [HttpPatch("state")]
        public async Task<ActionResult> ChangeState(ChangeStateCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id) => Ok(await Mediator.Send(new GetBugByIdQuery {Id = id}));
    }
}