using BLL.Paging;
using MediatR;

namespace BLL.Activities.Queries.GetActivitiesList
{
    public class GetActivityListQuery : IRequest<Result<PagedList<ActivityDto>>>
    {
        public ActivityParams Params { get; set; }
    }
}
