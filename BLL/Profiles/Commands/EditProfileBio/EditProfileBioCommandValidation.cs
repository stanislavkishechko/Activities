using FluentValidation;

namespace BLL.Profiles.Commands.EditProfileBio
{
    public class EditProfileBioCommandValidation : AbstractValidator<EditProfileBioCommand>
    {
        public EditProfileBioCommandValidation()
        {
            RuleFor(x => x.DisplayName).NotEmpty();
        }
    }
}
