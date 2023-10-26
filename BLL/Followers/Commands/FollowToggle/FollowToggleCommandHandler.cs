using BLL.Interfaces;
using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Followers.Commands.FollowToggle
{
    public class FollowToggleCommandHandler : IRequestHandler<FollowToggleCommand, Result<Unit>>
    {
        private readonly DataContext _dbContext;
        private readonly IUserAccessor _userAccessor;

        public FollowToggleCommandHandler(DataContext dbContext, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }
        public async Task<Result<Unit>> Handle(FollowToggleCommand request, CancellationToken cancellationToken)
        {
            var observer = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.UserName == _userAccessor.GetUserName());

            var target = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.UserName == request.TargetUsername);

            if (target == null) return null;

            var following = await _dbContext.UserFollowings.FindAsync(observer.Id, target.Id);

            if (following == null)
            {
                following = new UserFollowing
                {
                    Observer = observer,
                    Target = target
                };

                _dbContext.UserFollowings.Add(following);
            }
            else
            {
                _dbContext.UserFollowings.Remove(following);
            }

            var success = await _dbContext.SaveChangesAsync() > 0;

            if (success) return Result<Unit>.Success(Unit.Value);

            return Result<Unit>.Failure("Failed to update following");
        }
    }
}
