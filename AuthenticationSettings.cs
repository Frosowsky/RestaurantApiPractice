namespace WebApplication3
{
    public class AuthenticationSettings
    {
        public string JwtKey { get; set; }
        public int JwtExpiteDays { get; set; }
        public String JwtIssuer { get; set; }
    }
}
