using DAL.Domain.Entities;
using MediatR;

namespace BLL.Activities.Commands.CreateActivity
{
    public class CreateActivityCommand : IRequest<Result<Unit>>
    {
        public Activity Activity { get; set; }
    }
}
