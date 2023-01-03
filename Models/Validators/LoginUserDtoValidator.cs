using FluentValidation;
using WebApplication3.Entitie;

namespace WebApplication3.Models.Validators
{
    public class LoginUserDtoValidator : AbstractValidator<LoginDto>
    
    {
        public LoginUserDtoValidator()
        {
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        }


    }
}
