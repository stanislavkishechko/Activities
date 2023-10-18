using MediatR;

namespace BLL.Profiles.Queries
{
    public class GetProfileDetailsQuery : IRequest<Result<Profile>>
    {
        public string Username { get; set; }
    }
}
