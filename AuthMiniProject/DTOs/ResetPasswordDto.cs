using System.ComponentModel.DataAnnotations;

namespace WebApplication25.DTOs;

public class ResetPasswordDto
{
    [Required(ErrorMessage = "وارد کردن ایمیل الزامی است.")]
    [EmailAddress(ErrorMessage = "فرمت ایمیل معتبر نیست.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "وارد کردن کد بازیابی الزامی است.")]
    [StringLength(6, MinimumLength = 6, ErrorMessage = "کد بازیابی باید دقیقاً ۶ رقم باشد.")]
    public string Code { get; set; } = string.Empty;

    [Required(ErrorMessage = "رمز عبور جدید الزامی است.")]
    [MinLength(8, ErrorMessage = "رمز عبور باید حداقل ۸ کاراکتر باشد.")]
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", 
        ErrorMessage = "رمز عبور جدید باید شامل حروف بزرگ، کوچک، عدد و کاراکتر خاص باشد.")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "تکرار رمز عبور جدید الزامی است.")]
    [Compare("NewPassword", ErrorMessage = "رمز عبور جدید و تکرار آن مطابقت ندارند.")]
    public string ConfirmNewPassword { get; set; } = string.Empty;
}