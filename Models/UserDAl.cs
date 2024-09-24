namespace TacoFastFoodAPI.Models
{
    public class UserDAl
    {
        public static bool ValidateKey(string apiKey)
        {
            FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();
            return dbContext.Users.Any(u => u.ApiKey == apiKey);
        }
    }
}
