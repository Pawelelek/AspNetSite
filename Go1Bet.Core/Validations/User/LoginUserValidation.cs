using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Core.DTO_s.User;

namespace Go1Bet.Core.Validations.User
{
    public class LoginUserValidation : AbstractValidator<LoginUserDto>
    {
        public LoginUserValidation()
        {
            RuleFor(r => r.Email).EmailAddress().NotEmpty();
            RuleFor(r => r.Password).MinimumLength(6).NotEmpty();
        }
    }
}
