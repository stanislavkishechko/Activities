using MediatR;

namespace BLL.Photos.DeletePhoto
{
    public class DeletePhotoCommand : IRequest<Result<Unit>>
    {
        public string Id { get; set; }
    }
}
