using DAL.Domain.Entities;
using MediatR;

namespace BLL.Activities.Commands.CreateActivity
{
    public class CreateActivityCommand : IRequest
    {
        public Activity Activity { get; set; }
    }
}
