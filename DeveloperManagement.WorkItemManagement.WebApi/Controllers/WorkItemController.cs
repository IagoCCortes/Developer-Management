using System.Threading.Tasks;
using DeveloperManagement.WorkItemManagement.Application.WorkItems.Commands.AddBug;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperManagement.WorkItemManagement.WebApi.Controllers
{
    public class WorkItemController : ApiControllerBase
    {
        [HttpPost("bug")]
        public async Task<ActionResult> AddBug(AddBugCommand command) => Ok(await Mediator.Send(command));
    }
}