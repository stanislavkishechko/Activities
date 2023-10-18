using AutoMapper;
using AutoMapper.QueryableExtensions;
using DAL.Db;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Profiles.Queries
{
    public class GetProfileDetailsQueryHandler : IRequestHandler<GetProfileDetailsQuery, Result<Profile>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public GetProfileDetailsQueryHandler(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<Profile>> Handle(GetProfileDetailsQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                     .ProjectTo<Profile>(_mapper.ConfigurationProvider)
                     .SingleOrDefaultAsync(x => x.Username == request.Username);

            if (user == null) return null;

            return Result<Profile>.Success(user);
        }
    }
}
