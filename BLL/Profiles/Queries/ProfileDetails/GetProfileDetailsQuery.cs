using MediatR;

namespace BLL.Profiles.Queries.ProfileDetails
{
    public class GetProfileDetailsQuery : IRequest<Result<Profile>>
    {
        public string Username { get; set; }
    }
}
