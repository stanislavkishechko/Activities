using DAL.Domain.Entities;
using MediatR;

namespace BLL.Activities.Queries.GetActivityDetails
{
    public class GetActivityDetailsQuery : IRequest<Activity>
    {
        public Guid Id { get; set; }
    }
}
