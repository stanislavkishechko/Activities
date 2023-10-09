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
        public async Task<IActionResult> GetActivies()
        {
            var result = await Mediator.Send(new GetActivityListQuery());

            return HandleResult(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetActivity(Guid id)
        {
            var result = await Mediator.Send(new GetActivityDetailsQuery { Id = id });

            return HandleResult(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateActivity(Activity activity)
        {
            var result = await Mediator.Send(new CreateActivityCommand { Activity = activity });
            return HandleResult(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
        {
            activity.Id = id;

            var result = await Mediator.Send(new UpdateActivityCommand { Activity = activity });

            return HandleResult(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var result = await Mediator.Send(new DeleteActivityCommand { Id = id });

            return HandleResult(result);
        }
    }
}
