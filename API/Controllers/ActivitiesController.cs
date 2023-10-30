using BLL.Activities;
using BLL.Activities.Commands.CreateActivity;
using BLL.Activities.Commands.DeleteActivity;
using BLL.Activities.Commands.UpdateActivity;
using BLL.Activities.Queries.GetActivitiesList;
using BLL.Activities.Queries.GetActivityDetails;
using BLL.Attendees.Commands.UpdateAttendees;
using DAL.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ActivitiesController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetActivies([FromQuery]ActivityParams param)
        {
            var result = await Mediator.Send(new GetActivityListQuery { Params = param});

            return HandlePagedResult(result);
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

        [Authorize(Policy = "IsActivityHost")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateActivity(Guid id, Activity activity)
        {
            activity.Id = id;

            var result = await Mediator.Send(new UpdateActivityCommand { Activity = activity });

            return HandleResult(result);
        }

        [Authorize(Policy = "IsActivityHost")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(Guid id)
        {
            var result = await Mediator.Send(new DeleteActivityCommand { Id = id });

            return HandleResult(result);
        }

        [HttpPost("{id}/attend")]
        public async Task<IActionResult> Attend(Guid id)
        {
            var result = await Mediator.Send(new UpdateAttendanceCommand { Id = id });

            return HandleResult(result);
        }
    }
}
