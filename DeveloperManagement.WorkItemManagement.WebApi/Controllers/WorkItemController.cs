using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.AddBug;
using DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.SpecifyBugInfo;
using DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.UpdateBug;
using DeveloperManagement.WorkItemManagement.Application.Bugs.Queries.GetBugById;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperManagement.WorkItemManagement.WebApi.Controllers
{
    public class WorkItemController : ApiControllerBase
    {
        [HttpPost("bug")]
        public async Task<ActionResult> AddBug(CreateBugCommand command) => Ok(await Mediator.Send(command));
        
        [HttpPatch("bug/planning")]
        public async Task<ActionResult> ModifyBugPlanning(ModifyBugPlanningCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }
        
        [HttpPatch("bug/info")]
        public async Task<ActionResult> SpecifyBugInfo(SpecifyBugInfoCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id) => Ok(await Mediator.Send(new GetBugByIdQuery {Id = id}));
    }
}