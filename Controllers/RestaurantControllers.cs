using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Entitie;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    public class RestaurantControllers : ControllerBase
    {
        private readonly IRestaurantService _restaurantService;
        private readonly RestaurantDbContext _dbContext;
        private readonly IMapper _mapper;
        public RestaurantControllers(IRestaurantService restaurantService)
        {
            _restaurantService = restaurantService;
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
           var isDeleted = _restaurantService.Delete(id);

            if(isDeleted)
            {
                return NoContent();
            }

            return NotFound();
        }
        
        [HttpGet]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {
            var restaurantDtos = _restaurantService.GetAll();
            return Ok(restaurantDtos);
        }   
        [HttpPost]
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            if(!ModelState.IsValid) { return BadRequest(ModelState); }
            var id = _restaurantService.Create(dto);
            return Created($"/api/restaurant/{id}", null);
        }
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get ([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetByIdd(id);
            if(restaurant == null)
            {
                return NotFound();
            }

            
            return Ok(restaurant);
        }
    }

    
}
