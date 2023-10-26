using AutoMapper;
using MediatR;

namespace BLL.Followers.Queries.GetFollowersList
{
    public class GetFollowersListQuery : IRequest<Result<List<Profiles.Profile>>>
    {
        public string Predicate { get; set; }
        public string Username { get; set; }
    }
}
