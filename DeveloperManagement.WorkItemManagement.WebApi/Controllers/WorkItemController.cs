using System;
using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Application.Bugs.Commands.AddBug;
using DeveloperManagement.WorkItemManagement.Application.Bugs.Queries.GetBugById;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperManagement.WorkItemManagement.WebApi.Controllers
{
    public class WorkItemController : ApiControllerBase
    {
        [HttpPost("bug")]
        public async Task<ActionResult> AddBug(CreateBugCommand command) => Ok(await Mediator.Send(command));
        
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id) => Ok(await Mediator.Send(new GetBugByIdQuery {Id = id}));
    }
}