using DAL.Db;
using DAL.Domain.Entities;
using MediatR;

namespace BLL.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsQueryHandler : IRequestHandler<GetActivityDetailsQuery, Activity>
    {
        private readonly DataContext _dbContext;

        public GetActivityDetailsQueryHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Activity> Handle(GetActivityDetailsQuery request, CancellationToken cancellationToken)
        {
            return await _dbContext.Activities.FindAsync(request.Id);
        }
    }
}
