using MediatR;

namespace BLL.Profiles.Commands.EditProfileBio
{
    public class EditProfileBioCommand : IRequest<Result<Unit>>
    {
        public string DisplayName { get; set; }
        public string Bio { get; set; }
    }
}
