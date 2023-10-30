using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Interfaces;
using BLL.Paging;
using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Activities.Queries.GetActivitiesList
{
    public class GetActivityListQueryHandler : IRequestHandler<GetActivityListQuery, Result<PagedList<ActivityDto>>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetActivityListQueryHandler(DataContext dbContext, IMapper mapper, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<PagedList<ActivityDto>>> Handle(GetActivityListQuery request, CancellationToken cancellationToken)
        {
            var query = _dbContext.Activities
                .Where( d => d.Date >= request.Params.StartDate)
                .OrderBy(d => d.Date)
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider, 
                    new {currentUserName = _userAccessor.GetUserName()})
                .AsQueryable();

            if (request.Params.IsGoing && !request.Params.IsHost)
            {
                query = query.Where(x => x.Attendees.Any(a => a.Username == _userAccessor.GetUserName()));
            }

            if (request.Params.IsHost && !request.Params.IsGoing)
            {
                query = query.Where(x => x.HostUsername == _userAccessor.GetUserName());
            }

            return Result<PagedList<ActivityDto>>.Success(
                await PagedList<ActivityDto>.CreateAsync(query, request.Params.PageNumber, request.Params.PageSize));
        }
    }
}
