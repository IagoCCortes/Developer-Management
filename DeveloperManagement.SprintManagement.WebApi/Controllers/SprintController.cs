using System;
using System.Threading.Tasks;
using DeveloperManagement.SprintManagement.Application.Sprints.Commands.AddCapacityToSprint;
using DeveloperManagement.SprintManagement.Application.Sprints.Commands.CreateSprint;
using DeveloperManagement.SprintManagement.Application.Sprints.Queries.GetSprintById;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperManagement.SprintManagement.WebApi.Controllers
{
    public class SprintController : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult> AddSprint(CreateSprintCommand command) => Ok(await Mediator.Send(command));
        
        [HttpPatch("addcapacity")]
        public async Task<ActionResult> AddCapacity(AddCapacityToSprintCommand command) => Ok(await Mediator.Send(command));

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id) => Ok(await Mediator.Send(new GetSprintByIdQuery {Id = id}));
    }
}