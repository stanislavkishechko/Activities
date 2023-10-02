using DAL.Domain.Entities;
using MediatR;

namespace BLL.Activities.Commands.UpdateActivity
{
    public class UpdateActivityCommand : IRequest
    {
        public Activity Activity { get; set; }
    }
}
