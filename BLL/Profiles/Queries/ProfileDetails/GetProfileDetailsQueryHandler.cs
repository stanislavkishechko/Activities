using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Interfaces;
using DAL.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Profiles.Queries.ProfileDetails
{
    public class GetProfileDetailsQueryHandler : IRequestHandler<GetProfileDetailsQuery, Result<Profile>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetProfileDetailsQueryHandler(DataContext dbContext, IMapper mapper, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<Profile>> Handle(GetProfileDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                     .ProjectTo<Profile>(_mapper.ConfigurationProvider,
                        new { currentUsername = _userAccessor.GetUserName() })
                     .SingleOrDefaultAsync(x => x.Username == request.Username);

            if (user == null) return null;

            return Result<Profile>.Success(user);
        }
    }
}
