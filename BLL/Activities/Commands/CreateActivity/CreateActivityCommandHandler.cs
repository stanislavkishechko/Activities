using DAL.Db;
using MediatR;

namespace BLL.Activities.Commands.CreateActivity
{
    public class CreateActivityCommandHandler : IRequestHandler<CreateActivityCommand>
    {
        private readonly DataContext _dbContext;

        public CreateActivityCommandHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            _dbContext.Activities.Add(request.Activity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
