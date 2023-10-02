using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Activities.Queries.GetActivitiesList
{
    public class GetActivityListQueryHandler : IRequestHandler<GetActivityListQuery, List<Activity>>
    {
        private readonly DataContext _dbContext;

        public GetActivityListQueryHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Activity>> Handle(GetActivityListQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Activities.ToListAsync();
        }
    }
}
