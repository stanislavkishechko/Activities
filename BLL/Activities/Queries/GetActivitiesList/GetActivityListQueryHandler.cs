using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Activities.Queries.GetActivitiesList
{
    public class GetActivityListQueryHandler : IRequestHandler<GetActivityListQuery, Result<List<ActivityDto>>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public GetActivityListQueryHandler(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<List<ActivityDto>>> Handle(GetActivityListQuery request, CancellationToken cancellationToken)
        {
            var activities = await _dbContext.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return Result<List<ActivityDto>>.Success(activities);
        }
    }
}
