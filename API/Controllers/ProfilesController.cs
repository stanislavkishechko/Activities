using BLL.Profiles.Commands.EditProfileBio;
using BLL.Profiles.Queries.ListActivities;
using BLL.Profiles.Queries.ProfileDetails;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ProfilesController : BaseApiController
    {
        [HttpGet("{username}")]
        public async Task<IActionResult> GetProfile(string username)
        {
            return HandleResult(await Mediator.Send(new GetProfileDetailsQuery { Username = username }));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(EditProfileBioCommand command)
        {
            return HandleResult(await Mediator.Send(command));
        }

        [HttpGet("{username}/activities")]
        public async Task<IActionResult> GetUserActivities(string username, string predicate)
        {
            return HandleResult(await Mediator.Send(new ListActivitiesQuery { Username = username, Predicate = predicate }));
        }
    }
}
