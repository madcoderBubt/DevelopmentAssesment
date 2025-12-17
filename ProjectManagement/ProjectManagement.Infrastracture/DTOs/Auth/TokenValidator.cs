using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.Infrastracture.DTOs.Auth
{
    public class TokenValidator : AbstractValidator<TokenRequest>
    {
        public TokenValidator() 
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Should not be empty")
                .EmailAddress().WithMessage("Must be valid email address.");

            RuleFor(x=> x.Password)
                .NotEmpty().WithMessage("Should not be empty")
                .Length(6, 15).WithMessage("Should be between 6 to 15 charecter")
                .Must(p=> p.Any(Char.IsUpper) && p.Any(Char.IsLower) && p.Any(Char.IsNumber)).WithMessage("Must contain 1 upper, 1 lower, 1 number value");
        }

    }
}
