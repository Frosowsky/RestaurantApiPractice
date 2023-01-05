using FluentValidation;

namespace WebApplication3.Models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = new int[] { 5, 10, 15 };
        public RestaurantQueryValidator()
        {
            RuleFor ( c => c.PageNumber).GreaterThanOrEqualTo(1);
            RuleFor(c => c.PageNumber).Custom((value, context) =>
            {
                if (!allowedPageSizes.Contains(value))
                {
                    context.AddFailure("PageSize", $"PageSize need be in {string.Join(",", allowedPageSizes)}");
                }
            });
        }
    }
}
