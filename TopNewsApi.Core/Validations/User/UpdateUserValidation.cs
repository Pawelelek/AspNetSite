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
            RuleFor(r => r.PhoneNumber).MaximumLength(20);
        }
    }
}
