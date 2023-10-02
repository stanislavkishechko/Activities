using DAL.Domain.Entities;
using MediatR;

namespace BLL.Activities.Queries.GetActivitiesList
{
    public class GetActivityListQuery : IRequest<List<Activity>>
    {

    }
}
