using MediatR;

namespace BLL.Photos.SetMainPhoto
{
    public class SetMainPhotoCommand : IRequest<Result<Unit>>
    {
        public string Id { get; set; }
    }
}
