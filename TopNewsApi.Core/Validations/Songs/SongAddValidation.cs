using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TopNewsApi.Core.DTO_s.User;

namespace TopNewsApi.Core.Validations.Songs
{
    public class SongAddValidation : AbstractValidator<SongsDto>
    {
        public SongAddValidation()
        {
            RuleFor(r => r.SongUrl).NotEmpty();
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}
