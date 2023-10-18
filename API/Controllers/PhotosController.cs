using BLL.Photos.CreatePhoto;
using BLL.Photos.DeletePhoto;
using BLL.Photos.SetMainPhoto;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class PhotosController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreatePhotoCommand photoCommand)
        {
            return HandleResult(await Mediator.Send(photoCommand));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return HandleResult(await Mediator.Send(new DeletePhotoCommand { Id = id }));
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMain(string id)
        {
            return HandleResult(await Mediator.Send(new SetMainPhotoCommand { Id = id }));
        }
    }
}
