using MediatR;

namespace BLL.Followers.Commands.FollowToggle
{
    public class FollowToggleCommand : IRequest<Result<Unit>>
    {
        public string TargetUsername { get; set; }
    }
}
