using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.Token;
using TopNewsApi.Core.Entities.Tokens;

namespace TopNewsApi.Core.Validations.Token
{
    public class TokenRequestValidation : AbstractValidator<TokenRequestDto>
    {
        public TokenRequestValidation()
        {
            RuleFor(r => r.Token).NotEmpty();
            RuleFor(r => r.RefreshToken).NotEmpty();
        }
    }
}
