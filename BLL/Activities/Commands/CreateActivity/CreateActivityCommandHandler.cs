using BLL.Interfaces;
using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Activities.Commands.CreateActivity
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Result<Unit>>
    {
        private readonly DataContext _dbContext;
        private readonly IUserAccessor _userAccessor;

        public CreateActivityCommandHandler(DataContext dbContext, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Unit>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.UserName == _userAccessor.GetUserName());

            var attendee = new ActivityAttendee
            {
                AppUser = user,
                Activity = request.Activity,
                IsHost = true
            };

            request.Activity.Attendees.Add(attendee);

            _dbContext.Activities.Add(request.Activity);
            
            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to cteate activity");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
