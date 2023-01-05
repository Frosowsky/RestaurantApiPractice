using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Authorization
{
    public class MinimumAgeRequirement : IAuthorizationRequirement
    {
        public int MinimumAge { get; }
        public MinimumAgeRequirement(int minimumAge)
        {
            MinimumAge= minimumAge;
        }

    }
}
