using FluentValidation;
using Go1Bet.Infrastructure.DTO_s.User;

namespace Go1Bet.Infrastructure.Validations.User
{
    public class UpdateUserValidation : AbstractValidator<UserEditPersonalInfoDTO>
    {
        public UpdateUserValidation()
        {
            //RuleFor(r => r.FirstName).NotEmpty().MaximumLength(64).MinimumLength(2);
            //RuleFor(r => r.LastName).NotEmpty().MaximumLength(64).MinimumLength(2);
            //RuleFor(r => r.PhoneNumber).MaximumLength(20);
        }
    }
}
