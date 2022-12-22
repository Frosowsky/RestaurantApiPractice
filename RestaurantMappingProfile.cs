using AutoMapper;
using Microsoft.EntityFrameworkCore.Update;
using WebApplication3.Entitie;
using WebApplication3.Models;

namespace WebApplication3
{
    public class RestaurantMappingProfile : Profile

    {
        public RestaurantMappingProfile()
        {
            CreateMap<Restaurant, RestaurantDto>()
                .ForMember(m => m.City, c => c.MapFrom(s => s.Adress.City))
                .ForMember(m => m.Street, c => c.MapFrom(s => s.Adress.Street))
                .ForMember(m => m.PostalCode, c => c.MapFrom(s => s.Adress.PostalCode));

            CreateMap<Dish, DishDto>();
        }
    }
}
