using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entitie;
using WebApplication3.Exceptions;
using WebApplication3.Models;

namespace WebApplication3.Services
{
    public class RestaurantService : IRestaurantService
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<RestaurantService> _logger;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }
        public RestaurantDto GetByIdd(int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .Include(r => r.Adress)
                .Include(r => r.Dishes)
                .FirstOrDefault(x => x.Id == id);
            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            var result = _mapper.Map<RestaurantDto>(restaurant);
            return result;
        }

        public IEnumerable<RestaurantDto> GetAll()
        {
            var resturant = _dbContext
              .Restaurants
              .Include(r => r.Adress)
              .Include(r => r.Dishes)
              .ToList();
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(resturant);
            return restaurantsDtos;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();

            return restaurant.Id;
        }

        public void Delete(int id)
        {
           
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == id);

            if(restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            _dbContext.Restaurants.Remove(restaurant);
            _dbContext.SaveChanges();

          
        }

        public void Update(int id, UpdateRestaurantDto dto)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id==id);

            if (restaurant is null)
            {
                throw new NotFoundException("Restaurant not found");
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
          
        }
    }
}
