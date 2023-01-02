using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("/api/restaurant/{restaurantId}/dish")]
    [ApiController]
    public class DishController : ControllerBase
    {
        private readonly IDishService _dishService;

        public DishController(IDishService dishService)
        {
            _dishService = dishService;
        }
        [HttpPost]
         public ActionResult Post([FromRoute] int restaurantId, [FromBody] CreateDishDto dto) 
        {
           var newDishId = _dishService.Create(restaurantId, dto);

            return Created($"/api/restaurant/{restaurantId}/dish/{newDishId}", null);
        }

        [HttpGet]
        public ActionResult<List<DishDto>> Get([FromRoute]int restaurantId) 
        {
            var result = _dishService.GetAll(restaurantId);
            return Ok(result);
        }

        [HttpGet("{dishId}")]
        public ActionResult<DishDto> Get([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            DishDto dish = _dishService.GetById(restaurantId, dishId);
            return Ok(dish);
        }

        [HttpDelete]
        public ActionResult Delete([FromRoute] int restaurantId)
        {
            _dishService.RemoveAll(restaurantId);
            return NoContent();
        }

        [HttpDelete("{dishId}")]
        public ActionResult DeleteOne([FromRoute] int restaurantId, [FromRoute] int dishId)
        {
            _dishService.RemoveOne(restaurantId, dishId);
            return NoContent();
        }

    }
}
