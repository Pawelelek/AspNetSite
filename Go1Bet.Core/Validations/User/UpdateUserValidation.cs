using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.DTO_s.User;

namespace Go1Bet.Core.Validations.User
{
    public class UpdateUserValidation : AbstractValidator<UserEditDTO>
    {
        public UpdateUserValidation()
        {
            RuleFor(r => r.FirstName).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.LastName).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.PhoneNumber).MaximumLength(20);
        }
    }
}
