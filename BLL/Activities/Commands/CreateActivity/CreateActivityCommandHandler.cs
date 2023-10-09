using DAL.Db;
using MediatR;

namespace BLL.Activities.Commands.CreateActivity
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand, Result<Unit>>
    {
        private readonly DataContext _dbContext;

        public CreateActivityCommandHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Unit>> Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Activities.Add(request.Activity);
            
            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failed to cteate activity");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
