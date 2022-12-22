using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Entitie;

namespace WebApplication3.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantControllers : ControllerBase
    {
        private readonly RestaurantDbContext _dbContext;
        public RestaurantControllers(RestaurantDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Restaurant>> GetAll()
        {
            var resturant = _dbContext
                .Restaurants
                .ToList();

            return Ok(resturant);
        }

        [HttpGet("{id}")]
        public ActionResult<Restaurant> Get ([FromRoute] int id)
        {
            var restaurant = _dbContext
                .Restaurants
                .FirstOrDefault(x => x.Id == id);
            if(restaurant == null)
            {
                return NotFound();
            } else return Ok(restaurant);
        }
    }

    
}
