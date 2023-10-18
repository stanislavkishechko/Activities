using DAL.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.Photos.CreatePhoto
{
    public class CreatePhotoCommand : IRequest<Result<Photo>>
    {
        public IFormFile File { get; set; }
    }
}
