using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Authorization
{
    public class AddMinimum : IAuthorizationRequirement
    {
        public int RestaurantValue { get; }
        public AddMinimum(int restaurantValue)
        {
            RestaurantValue = restaurantValue;
        }
    }
}
