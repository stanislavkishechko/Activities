using BLL.Activities.Commands.CreateActivity;
using BLL.Activities.Commands.DeleteActivity;
using BLL.Activities.Commands.UpdateActivity;
using BLL.Activities.Queries.GetActivitiesList;
using BLL.Activities.Queries.GetActivityDetails;
using DAL.Db;
using DAL.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<Activity>>> GetActivies()
        {
            return await Mediator.Send(new GetActivityListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(Guid id)
        {
            return await Mediator.Send(new GetActivityDetailsQuery { Id = id });
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            await Mediator.Send(new CreateActivityCommand { Activity = activity });
            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
        {
            activity.Id = id;

            await Mediator.Send(new UpdateActivityCommand { Activity = activity });

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            await Mediator.Send(new DeleteActivityCommand { Id = id });

            return Ok();
        }
    }
}
