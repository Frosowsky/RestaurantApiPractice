using FluentValidation;
using WebApplication3.Entitie;

namespace WebApplication3.Models.Validators
{
    public class RestaurantQueryValidator : AbstractValidator<RestaurantQuery>
    {
        private int[] allowedPageSizes = new int[] { 5, 10, 15 };
        private string[] allowedSortByColumnNames = { nameof(Restaurant.Name), nameof(Restaurant.Description), nameof(Restaurant.Category) };
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
            RuleFor(c => c.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
                .WithMessage($"Sort by is optional, or must be in{string.Join(",", allowedSortByColumnNames)}");
        }
    }
}
