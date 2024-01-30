using RentStudio.DataAccesLayer;

namespace RentStudio.Helpers.JwtUtils
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User user);
        public Guid? GetUserId(string? token);
    }
}
