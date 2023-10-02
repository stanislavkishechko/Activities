using AutoMapper;
using DAL.Db;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Activities.Commands.UpdateActivity
{
    public class UpdateActivityCommandHandler : IRequestHandler<UpdateActivityCommand>
    {
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public UpdateActivityCommandHandler(DataContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            var activity = await _dbContext.Activities.FindAsync(request.Activity.Id);

            _mapper.Map(request.Activity, activity);

            activity.Title = request.Activity.Title ?? activity.Title;

            await _dbContext.SaveChangesAsync();
        }
    }
}
