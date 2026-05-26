using System.ComponentModel.DataAnnotations;

namespace WebApplication25.Entity;

public class User
{
    
    [Key]
    public int Id { get; set; }
    public string Email { get; set; } = string.Empty;
    
    public string PasswordHash { get; set; } = string.Empty;
        
    public string Role { get; set; } = "User"; 
        
    public bool IsEmailVerified { get; set; } = false;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public ICollection<UserToken> Tokens { get; set; } = new List<UserToken>();
}