using Microsoft.EntityFrameworkCore;
using WebApplication25.db;
using WebApplication25.Entity;

namespace WebApplication25.Services;

public class TokenService : ITokenService
{
        private readonly AppDbContext _context;

        public TokenService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> GenerateAndSaveEmailVerificationTokenAsync(User user)
        {

            var random = new Random();
            string verificationCode = random.Next(100000, 999999).ToString();
            
            Console.WriteLine(verificationCode);

            var userToken = new UserToken
            {
                TokenValue = verificationCode,
                TokenType = "EmailVerification",
                ExpiresAt = DateTime.UtcNow.AddSeconds(90),
                IsUsed = false,
                UserId = user.Id
            };


            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync();

            return verificationCode;
        }

        public async Task<bool> VerifyEmailTokenAsync(string email, string code)
        {

            string cleanEmail = email.Trim().ToLower();
            var tokenRecord = await _context.UserTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.User.Email ==  cleanEmail
                                          && t.TokenValue == code 
                                          && t.TokenType == "EmailVerification"
                                          && !t.IsUsed 
                                          && t.ExpiresAt > DateTime.UtcNow);

            if (tokenRecord == null)
                return false;

      
            tokenRecord.IsUsed = true;
            
           
            tokenRecord.User.IsEmailVerified = true;

            await _context.SaveChangesAsync();
            return true;
        }
        
        public async Task<string> GenerateAndSaveResetPasswordTokenAsync(User user)
        {
            var random = new Random();
            string resetCode = random.Next(100000, 999999).ToString();
            
            Console.WriteLine(resetCode);


            var userToken = new UserToken
            {
                TokenValue = resetCode,
                TokenType = "ResetPassword", 
                ExpiresAt = DateTime.UtcNow.AddSeconds(90), 
                IsUsed = false,
                UserId = user.Id
            };

            _context.UserTokens.Add(userToken);
            await _context.SaveChangesAsync();

            return resetCode;
        }

        public async Task<bool> VerifyResetPasswordTokenAsync(string email, string code)
        {
            string cleanEmail = email.Trim().ToLower();

            var tokenRecord = await _context.UserTokens
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.User.Email == cleanEmail 
                                          && t.TokenValue == code 
                                          && t.TokenType == "ResetPassword"
                                          && !t.IsUsed 
                                          && t.ExpiresAt > DateTime.UtcNow);

            if (tokenRecord == null)
                return false;

           
            tokenRecord.IsUsed = true;
            await _context.SaveChangesAsync();
            return true;
        }
    
}