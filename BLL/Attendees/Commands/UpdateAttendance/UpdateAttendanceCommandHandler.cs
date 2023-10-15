using BLL.Attendees.Commands.UpdateAttendees;
using BLL.Interfaces;
using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Attendees.Commands.UpdateAttendance
{
    public class UpdateAttendanceCommandHandler : IRequestHandler<UpdateAttendanceCommand, Result<Unit>>
    {
        private readonly DataContext _dbContext;
        private readonly IUserAccessor _userAccessor;

        public UpdateAttendanceCommandHandler(DataContext dbContext, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _userAccessor = userAccessor;
        }
        public async Task<Result<Unit>> Handle(UpdateAttendanceCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dbContext.Activities
                .Include(a => a.Attendees).ThenInclude(u => u.AppUser)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (activity == null) return null;

            var user = await _dbContext.Users.FirstOrDefaultAsync(x =>
                x.UserName == _userAccessor.GetUserName());

            if (user == null) return null;

            var hostUsername = activity.Attendees.FirstOrDefault(x => x.IsHost)?.AppUser?.UserName;

            var attendance = activity.Attendees.FirstOrDefault(x => x.AppUser.UserName == user.UserName);

            if (attendance != null && hostUsername == user.UserName)
            {
                activity.IsCancelled = !activity.IsCancelled;
            }

            if (attendance != null && hostUsername != user.UserName)
            {
                activity.Attendees.Remove(attendance);
            }

            if (attendance == null)
            {
                attendance = new ActivityAttendee
                {
                    AppUser = user,
                    Activity = activity,
                    IsHost = false
                };

                activity.Attendees.Add(attendance);
            }

            var result = await _dbContext.SaveChangesAsync() > 0;

            return result ? Result<Unit>.Success(Unit.Value) : Result<Unit>.Failure("Problem updating attendance");


        }
    }
}
