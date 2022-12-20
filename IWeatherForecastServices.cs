namespace WebApplication3
{
    public interface IWeatherForecastServices
    {
        IEnumerable<WeatherForecast> Get(int Days, int minTemp, int maxTemp);

    }
}