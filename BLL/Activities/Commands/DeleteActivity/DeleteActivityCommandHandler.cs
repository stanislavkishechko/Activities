using DAL.Db;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Activities.Commands.DeleteActivity
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand, Result<Unit>>
    {
        private readonly DataContext _dbContext;

        public DeleteActivityCommandHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<Unit>> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dbContext.Activities.FindAsync(request.Id);

            if (activity == null) return null;

            _dbContext.Remove(activity);

            var result = await _dbContext.SaveChangesAsync() > 0;

            if (!result) return Result<Unit>.Failure("Failde to delete the activity");

            return Result<Unit>.Success(Unit.Value);
        }
    }
}
