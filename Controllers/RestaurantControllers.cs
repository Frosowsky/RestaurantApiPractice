using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entitie;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantControllers : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestaurantControllers(RestaurantDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var resturant = _dbContext
                .Restaurants
                .Include(r => r.Adress)
                .Include(r => r.Dishes)
                .ToList();
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(resturant);
            return Ok(restaurantsDtos);
        }
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            var restaurant = _mapper.Map<Restaurant>(dto);
            _dbContext.Restaurants.Add(restaurant);
            _dbContext.SaveChanges();
            return Created($"/api/restaurant/{restaurant.Id}", null);
        }
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get ([FromRoute] int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if(restaurant == null)
            {
                return NotFound();
            }

            var restaurantDto = _mapper.Map<Restaurant>(restaurant);
            return Ok(restaurantDto);
        }
    }

    
}
