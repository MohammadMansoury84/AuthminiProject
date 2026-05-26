using System.ComponentModel.DataAnnotations;

namespace WebApplication25.DTOs;

public class RegisterDto
{
    [Required(ErrorMessage = "ایمیل الزامی است.")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل وارد شده معتبر نیست.")]
    [MaxLength(150, ErrorMessage = "ایمیل نمی‌تواند بیشتر از ۱۵۰ کاراکتر باشد.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "رمز عبور الزامی است.")]
    [MinLength(8, ErrorMessage = "رمز عبور باید حداقل ۸ کاراکتر باشد.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
        ErrorMessage = "رمز عبور باید شامل حروف بزرگ، کوچک، عدد و کاراکتر خاص (@$!%*?&) باشد.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "تکرار رمز عبور الزامی است.")]
    [Compare("Password", ErrorMessage = "رمز عبور و تکرار آن با هم مطابقت ندارند.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}