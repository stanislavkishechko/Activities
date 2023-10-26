using BLL.Profiles.Commands.EditProfileBio;
using BLL.Profiles.Queries;
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
    }
}
