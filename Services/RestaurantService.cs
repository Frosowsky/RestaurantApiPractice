using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;
using WebApplication3.Authorization;
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
        private readonly IAuthorizationService _authorizationService;
        private readonly IUserContextService _userContextService;

        public RestaurantService(RestaurantDbContext dbContext, IMapper mapper, ILogger<RestaurantService> logger,
            IAuthorizationService authorizationService, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
            _authorizationService = authorizationService;
            _userContextService = userContextService;
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

        public PageResult<RestaurantDto> GetAll(RestaurantQuery query)
        {

            var baseQuery = _dbContext
              .Restaurants
              .Include(r => r.Adress)
              .Include(r => r.Dishes)
              .Where(r => query.SearchPharse == null || r.Name.ToLower().Contains(query.SearchPharse.ToLower())
              || r.Name.ToLower().Contains(query.SearchPharse.ToLower()));


            if (!string.IsNullOrEmpty(query.SortBy))
            
            {
                var columnsSelectors = new Dictionary<string, Expression<Func<Restaurant, object>>>
                {
                    {nameof(Restaurant.Name), r=> r.Name},
                    {nameof(Restaurant.Description), r=> r.Description},
                    {nameof(Restaurant.Category), r=> r.Category}
                };

                var selectedColumn = columnsSelectors[query.SortBy];

               baseQuery = query.SortDirection == SortDirection.ASC? 
                    baseQuery.OrderBy(selectedColumn) 
                    : baseQuery.OrderByDescending(selectedColumn);
            }


            var resturant = baseQuery
              .Skip(query.PageSize * (query.PageNumber -1))
              .Take(query.PageSize)
              .ToList();

            
            var restaurantsDtos = _mapper.Map<List<RestaurantDto>>(resturant);

            var result = new PageResult<RestaurantDto>(restaurantsDtos, baseQuery.Count(), query.PageSize, query.PageNumber) ;
            return result;
        }

        public int Create(CreateRestaurantDto dto)
        {
            var restaurant = _mapper.Map<Restaurant>(dto);
            restaurant.CreatedById = _userContextService.GetUserId;
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
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Something wrong");
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
            var authorizationResult = _authorizationService.AuthorizeAsync(_userContextService.User, restaurant, new ResourceOperationRequirement(ResourceOperation.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException("Something wrong");
            }

            restaurant.Name = dto.Name;
            restaurant.Description = dto.Description;
            restaurant.HasDelivery = dto.HasDelivery;

            _dbContext.SaveChanges();
          
        }
    }
}
