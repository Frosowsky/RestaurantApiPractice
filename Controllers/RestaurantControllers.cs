﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text.RegularExpressions;
using WebApplication3.Entitie;
using WebApplication3.Models;
using WebApplication3.Services;

namespace WebApplication3.Controllers
{
    [Route("api/restaurant")]
    [ApiController]
    [Authorize]
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
            _restaurantService.Delete(id, User);
      
        return NoContent();
        }

        [HttpPut("{id}")]

        public ActionResult Update([FromBody] UpdateRestaurantDto dto, [FromRoute] int id)
        {
           
           _restaurantService.Update(id, dto, User);
            
            return Ok();

        }
        
        [HttpGet]
        [Authorize(Policy = "Atleast20")]
        public ActionResult<IEnumerable<RestaurantDto>> GetAll()
        {   

            var restaurantDtos = _restaurantService.GetAll();
            return Ok(restaurantDtos);
        }   
        [HttpPost]
        [Authorize(Roles = "Admin,Manager")]
        
        public ActionResult CreateRestaurant([FromBody]CreateRestaurantDto dto)
        {
            var userId =int.Parse( User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var id = _restaurantService.Create(dto, userId);
            return Created($"/api/restaurant/{id}", null);
        }
        [HttpGet("{id}")]
        public ActionResult<RestaurantDto> Get ([FromRoute] int id)
        {
            var restaurant = _restaurantService.GetByIdd(id);
                        
            return Ok(restaurant);
        }
    }

    
}
