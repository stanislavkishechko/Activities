using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Activities.Queries.GetActivitiesList
{
    public class GetActivityListQueryHandler : IRequestHandler<GetActivityListQuery, Result<List<Activity>>>
    {
        private readonly DataContext _dbContext;

        public GetActivityListQueryHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<List<Activity>>> Handle(GetActivityListQuery request, CancellationToken cancellationToken)
        {
            var activities = await _dbContext.Activities.ToListAsync();

            return Result<List<Activity>>.Success(activities);
        }
    }
}
