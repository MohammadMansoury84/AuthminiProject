using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication25.Entity;

public class UserToken
{
    public int Id { get; set; }
    public string TokenValue { get; set; } = string.Empty;
    public string TokenType { get; set; } = string.Empty; 
        
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false; 

    public int UserId { get; set; }
    
    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;
}