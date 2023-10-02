using DAL.Db;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Activities.Commands.DeleteActivity
{
    public class DeleteActivityCommandHandler : IRequestHandler<DeleteActivityCommand>
    {
        private readonly DataContext _dbContext;

        public DeleteActivityCommandHandler(DataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dbContext.Activities.FindAsync(request.Id);

            _dbContext.Remove(activity);

            await _dbContext.SaveChangesAsync();
        }
    }
}
