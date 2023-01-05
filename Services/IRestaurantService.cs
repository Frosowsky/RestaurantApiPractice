using System.Security.Claims;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        PageResult<RestaurantDto> GetAll (RestaurantQuery query);
        RestaurantDto GetByIdd(int id);
        void Delete(int id);
        void Update(int id, UpdateRestaurantDto dto);
    }
}