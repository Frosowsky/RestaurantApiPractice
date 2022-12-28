using WebApplication3.Models;

namespace WebApplication3.Services
{
    public interface IRestaurantService
    {
        int Create(CreateRestaurantDto dto);
        IEnumerable<RestaurantDto> GetAll();
        RestaurantDto GetByIdd(int id);
        bool Delete(int id);
    }
}