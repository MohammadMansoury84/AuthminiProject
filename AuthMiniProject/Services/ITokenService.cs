using WebApplication25.Entity;

namespace WebApplication25.Services;

public interface ITokenService
{
    Task<string> GenerateAndSaveEmailVerificationTokenAsync(User user);
    Task<bool> VerifyEmailTokenAsync(string email, string code);
    
    Task<string> GenerateAndSaveResetPasswordTokenAsync(User user);
    
    Task<bool> VerifyResetPasswordTokenAsync(string email, string code);
}