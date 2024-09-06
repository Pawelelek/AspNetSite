using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Go1Bet.Infrastructure.DTO_s.Token;
using Go1Bet.Core.Entities.Tokens;

namespace Go1Bet.Infrastructure.Validations.Token
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
