using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.User;

namespace TopNewsApi.Core.Validations.User
{
    public class UpdateUserValidation : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidation()
        {
            RuleFor(r => r.FirstName).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.LastName).NotEmpty().MaximumLength(64).MinimumLength(2);
            RuleFor(r => r.Email).NotEmpty().EmailAddress().MaximumLength(128);
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6);
            RuleFor(r => r.ConfirmPassword).NotEmpty().MinimumLength(6).Equal(p => p.Password);
            RuleFor(r => r.PhoneNumber).MaximumLength(20);
        }
    }
}
