using BLL.Interfaces;
using DAL.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Photos.SetMainPhoto
{
    public class SetMainPhotoCommandHandler : IRequestHandler<SetMainPhotoCommand, Result<Unit>>
    {
        private readonly DataContext _dbContext;
        private readonly IUserAccessor _userAccessor;

        public SetMainPhotoCommandHandler(DataContext dbContext, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(SetMainPhotoCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.Include(x => x.Photos)
                .FirstOrDefaultAsync(x => x.UserName == _userAccessor.GetUserName());

            if (user == null) return null;

            var photo = user.Photos.FirstOrDefault(x => x.Id == request.Id);

            if (photo == null) return null;

            var currentMain = user.Photos.FirstOrDefault(x => x.IsMain);

            if (currentMain != null) currentMain.IsMain = false;

            photo.IsMain = true;

            var success = await _dbContext.SaveChangesAsync() > 0;

            if (success) return Result<Unit>.Success(Unit.Value);

            return Result<Unit>.Failure("Problem setting main photo");
        }
    }
}
