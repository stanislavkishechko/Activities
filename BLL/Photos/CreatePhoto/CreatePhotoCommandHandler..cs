using BLL.Interfaces;
using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Photos.CreatePhoto
{
    public class CreatePhotoCommandHandler : IRequestHandler<CreatePhotoCommand, Result<Photo>>
    {
        private readonly DataContext _dbContext;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessor _userAccessor;

        public CreatePhotoCommandHandler(DataContext dbContext, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _photoAccessor = photoAccessor;
            _userAccessor = userAccessor;
        }
        public async Task<Result<Photo>> Handle(CreatePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

            if (user == null) return null;

            var photoUploadResult = await _photoAccessor.AddPhoto(request.File);

            var photo = new Photo
            {
                Url = photoUploadResult.Url,
                Id = photoUploadResult.PublicId
            };

            if (!user.Photos.Any(x => x.IsMain)) photo.IsMain = true;

            user.Photos.Add(photo);

            var result = await _dbContext.SaveChangesAsync() > 0;

            if (result) return Result<Photo>.Success(photo);

            return Result<Photo>.Failure("Problem adding photo");
        }
    }
}
