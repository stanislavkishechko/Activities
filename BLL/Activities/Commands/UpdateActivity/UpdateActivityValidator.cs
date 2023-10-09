using FluentValidation;

namespace BLL.Activities.Commands.UpdateActivity
{
    public class UpdateActivityCommandValidator : AbstractValidator<UpdateActivityCommand>
    {
        public UpdateActivityCommandValidator()
        {
            RuleFor(a => a.Activity).SetValidator(new ActivityValidator());
        }
    }
}
