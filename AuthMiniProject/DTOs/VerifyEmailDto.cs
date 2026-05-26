using System.ComponentModel.DataAnnotations;

namespace WebApplication25.DTOs;

public class VerifyEmailDto
{
    [Required(ErrorMessage = "ایمیل الزامی است.")]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "کد تایید الزامی است.")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "کد تایید باید دقیقاً ۶ رقم باشد.")]
    public string VerificationCode { get; set; } = string.Empty;
}