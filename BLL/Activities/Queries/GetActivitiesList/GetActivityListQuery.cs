using MediatR;

namespace BLL.Activities.Queries.GetActivitiesList
{
    public class GetActivityListQuery : IRequest<Result<List<ActivityDto>>>
    {

    }
}
