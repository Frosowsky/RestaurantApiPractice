using FluentValidation;
using System.ComponentModel.DataAnnotations;
using WebApplication3.Entitie;

namespace WebApplication3.Models.Validators
{
    public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserDtoValidator(RestaurantDbContext dbcontext)
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(x => x.Password)
                .MinimumLength(6);

            RuleFor(x => x.ConfirmedPassword).Equal(e => e.Password);

            RuleFor(x => x.Email).Custom((value, context) =>
            {
              var emailNews =  dbcontext.Users.Any(e => e.Email == value);
                if (emailNews)
                {
                    context.AddFailure("Email", "That email is taken");
                }
            });
        }
    }
}
