using AutoMapper;
using AutoMapper.QueryableExtensions;
using BLL.Interfaces;
using DAL.Db;
using DAL.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsQueryHandler : IRequestHandler<GetActivityDetailsQuery, Result<ActivityDto>>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IUserAccessor _userAccessor;

        public GetActivityDetailsQueryHandler(DataContext dbContext, IMapper mapper, IUserAccessor userAccessor)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _userAccessor = userAccessor;
        }

        public async Task<Result<ActivityDto>> Handle(GetActivityDetailsQuery request, CancellationToken cancellationToken)
        {
            var activity =  await _dbContext.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider,
                    new {currentUsername = _userAccessor.GetUserName()})
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Result<ActivityDto>.Success(activity);
        }
    }
}
