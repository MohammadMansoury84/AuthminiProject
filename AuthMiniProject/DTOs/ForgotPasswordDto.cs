using System.ComponentModel.DataAnnotations;

namespace WebApplication25.DTOs;

public class ForgotPasswordDto
{
    [Required(ErrorMessage = "وارد کردن ایمیل الزامی است.")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست.")]
    public string Email { get; set; } = string.Empty;
}