using WebApplication25.Entity;

namespace WebApplication25.Services;

public interface IJwtProvider
{
    string GenerateToken(User user);
}