using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public GetActivityDetailsQueryHandler(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<Result<ActivityDto>> Handle(GetActivityDetailsQuery request, CancellationToken cancellationToken)
        {
            var activity =  await _dbContext.Activities
                .ProjectTo<ActivityDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return Result<ActivityDto>.Success(activity);
        }
    }
}
