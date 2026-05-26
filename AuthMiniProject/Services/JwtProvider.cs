using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WebApplication25.Entity;

namespace WebApplication25.Services;

public class JwtProvider : IJwtProvider
{
    private readonly IConfiguration _configuration;
    
    
    public JwtProvider(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role ?? "User")
        };
        
        string secretKey = _configuration["JwtSettings:SecretKey"] 
                           ?? throw new InvalidOperationException("خطا: کلید امنیتی JWT در تنظیمات برنامه تعریف نشده است.");
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        string issuer = _configuration["JwtSettings:Issuer"] ?? "DefaultIssuer";
        string audience = _configuration["JwtSettings:Audience"] ?? "DefaultAudience";
        
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2), 
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}