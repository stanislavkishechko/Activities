using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Interfaces;
using DAL.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Followers.Queries.GetFollowersList
{
    public class GetFollowersListQueryHandler : IRequestHandler<GetFollowersListQuery, Result<List<Profiles.Profile>>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetFollowersListQueryHandler(DataContext dbContext, IMapper mapper, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<List<Profiles.Profile>>> Handle(GetFollowersListQuery request, CancellationToken cancellationToken)
        {
            var profiles = new List<Profiles.Profile>();

            switch (request.Predicate)
            {
                case "followers":
                    profiles = await _dbContext.UserFollowings.Where(x => x.Target.UserName == request.Username)
                        .Select(u => u.Observer)
                        .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider,
                            new { currentUserName = _userAccessor.GetUserName() })
                        .ToListAsync();
                break;
                case "following":
                    profiles = await _dbContext.UserFollowings.Where(x => x.Observer.UserName == request.Username)
                        .Select(u => u.Target)
                        .ProjectTo<Profiles.Profile>(_mapper.ConfigurationProvider,
                            new { currentUsername = _userAccessor.GetUserName() })
                        .ToListAsync();
                break;
            }

            return Result<List<Profiles.Profile>>.Success(profiles);
        }
    }
}
