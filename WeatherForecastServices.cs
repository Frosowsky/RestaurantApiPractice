namespace WebApplication3
{
    public class WeatherForecastServices : IWeatherForecastServices
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        public IEnumerable<WeatherForecast> Get(int Days, int minTemp, int maxTemp )
        {
            return Enumerable.Range(1, Days).Select(index => new WeatherForecast
            {  
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(minTemp, maxTemp),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    
    }
}
