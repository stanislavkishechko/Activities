using BLL.Interfaces;
using DAL.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Profiles.Commands.EditProfileBio
{
    public class EditProfileBioCommandHandler : IRequestHandler<EditProfileBioCommand, Result<Unit>>
    {
        private readonly DataContext _dbContext;
        private readonly IUserAccessor _userAccessor;

        public EditProfileBioCommandHandler(DataContext dbContext, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(EditProfileBioCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.UserName == _userAccessor.GetUserName());

            user.Bio = request.Bio ?? user.Bio;
            user.DisplayName = request.DisplayName ?? user.DisplayName;

            _dbContext.Entry(user).State = EntityState.Modified;

            var success = await _dbContext.SaveChangesAsync() > 0;
            
            if (success) return Result<Unit>.Success(Unit.Value);
                        return Result<Unit>.Failure("Problem updating profile");
        }
    }
}
