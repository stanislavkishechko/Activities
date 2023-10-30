using MediatR;

namespace BLL.Profiles.Queries.ListActivities
{
    public class ListActivitiesQuery : IRequest<Result<List<UserActivityDto>>>
    {
        public string Username { get; set; }
        public string Predicate { get; set; }
    }
}
