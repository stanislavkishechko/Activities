using BLL.Interfaces;
using DAL.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Photos.DeletePhoto
{
    public class DeletePhotoCommandHandler : IRequestHandler<DeletePhotoCommand, Result<Unit>>
    {
        private readonly DataContext _dbContext;
        private readonly IPhotoAccessor _photoAccessor;
        private readonly IUserAccessor _userAccessor;

        public DeletePhotoCommandHandler(DataContext dbContext, IPhotoAccessor photoAccessor, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _photoAccessor = photoAccessor;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.Include(p => p.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

            if (user == null) return null;

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);
            
            if (photo == null) return null;

            if (photo.IsMain) return Result<Unit>.Failure("You cannot delete youi main photo");

            var result = await _photoAccessor.DeletePhoto(photo.Id);

            if (result == null) return Result<Unit>.Failure("Problem deleting photo from Cloudinary");

            user.Photos.Remove(photo);

            var success = await _dbContext.SaveChangesAsync() > 0;

            if (success) return Result<Unit>.Success(Unit.Value);

            return Result<Unit>.Failure("Problem deleting photo API");
        }
    }
}
