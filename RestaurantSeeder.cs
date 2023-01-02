using WebApplication3.Entitie;

namespace WebApplication3
{
    public class RestaurantSeeder
    {
        private readonly RestaurantDbContext _dbcontext;
        public RestaurantSeeder(RestaurantDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
     public void Seed()
        {
            if (_dbcontext.Database.CanConnect())
            {
                if (!_dbcontext.Roles.Any())
                {
                    var roles = GetRoles();
                    _dbcontext.Roles.AddRange(roles);
                    _dbcontext.SaveChanges();   
                }
                if (!_dbcontext.Restaurants.Any())
                {
                    var restaurants = GetRestaurants();
                    _dbcontext.Restaurants.AddRange(restaurants);
                    _dbcontext.SaveChanges();
                }
            }
        }

        private IEnumerable<Role> GetRoles()
        {
            var roles = new List<Role>()
            {
                new Role()
                {
                    Name = "User"
                },
                new Role()
                {
                    Name = "Manager"
                },
                new Role()
                {
                    Name = "Admin"
                }
            };
            return roles;
      
        }
     private static IEnumerable<Restaurant> GetRestaurants()
        {
            var restaurant = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Name = "KFC",
                    Category = "Fast Food",
                    Description = "Good food",
                    HasDelivery= true,
                    ContactEmail = "amav",
                    ContactNumber = "123123",
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Nuggets",
                            Description = "best",
                            Price = 15.20m,
                        },
                        new Dish()
                        {
                            Name = "Frytki",
                              Description = "best",
                            Price = 8.0m,
                        }
                    },
                    Adress= new Adress()
                    {
                        City = "Kraków",
                        Street = "Długa",
                        PostalCode = "30000"
                       
                    }
                },
                new Restaurant()
                {
                    Name = "McDonald",
                    Category = "Fast Food",
                    Description = "Good food",
                    HasDelivery= true,
                    ContactEmail = "amav",
                    ContactNumber = "123123",
                    Dishes = new List<Dish>()
                    {
                        new Dish()
                        {
                            Name = "Ziemniaki",
                              Description = "best",
                            Price = 23.20m,
                        },
                        new Dish()
                        {
                            Name = "Mieso",
                              Description = "best",
                            Price = 81.0m,
                        }
                    },
                    Adress= new Adress()
                    {
                        City = "Kraków",
                        Street = "Szewska",
                        PostalCode = "30000"

                    }

                }
            
            
            };

            return restaurant;
        }
    }
}
