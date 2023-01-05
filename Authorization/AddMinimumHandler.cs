using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using System.Security.Claims;
using WebApplication3.Entitie;

namespace WebApplication3.Authorization
{
    public class AddMinimumHandler : AuthorizationHandler<AddMinimum>
    {
        private readonly RestaurantDbContext _restaurantDbContext;
        public AddMinimumHandler(RestaurantDbContext restaurantDbContext)
        {
            _restaurantDbContext = restaurantDbContext;
        }


        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AddMinimum requirement)
        {
            var userId = int.Parse(context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);

          var createdRestaurantCount =  _restaurantDbContext
                .Restaurants
                .Count(r => r.CreatedById == userId);
            
            if(createdRestaurantCount >= requirement.RestaurantValue)
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
