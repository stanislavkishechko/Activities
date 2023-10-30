using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Profiles.Queries.ListActivities
{
    public class ListActivitiesQueryHandler : IRequestHandler<ListActivitiesQuery, Result<List<UserActivityDto>>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public ListActivitiesQueryHandler(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<UserActivityDto>>> Handle(ListActivitiesQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.ActivityAttendees
                .Where(u => u.AppUser.UserName == request.Username)
                .OrderBy(a => a.Activity.Date)
                .ProjectTo<UserActivityDto>(_mapper.ConfigurationProvider)
                .AsQueryable();

            var today = DateTime.UtcNow;

            query = request.Predicate switch
            {
                "past" => query.Where(a => a.Date <= today),
                "hosting" => query.Where(a => a.HostUsername == request.Username),
                _ => query.Where(a => a.Date >= today)
            };

            var activities = await query.ToListAsync();

            return Result<List<UserActivityDto>>.Success(activities);
        }       
    }
}
