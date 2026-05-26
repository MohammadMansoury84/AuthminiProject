using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication25.db;
using WebApplication25.DTOs;
using WebApplication25.Entity;
using WebApplication25.Services;

namespace WebApplication25.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly IPasswordHasher _passwordHasher;
    private readonly AppDbContext _context;
    private readonly ITokenService _tokenService;
    private readonly IJwtProvider _jwtProvider;

    public AuthController(AppDbContext context, IPasswordHasher passwordHasher, ITokenService tokenService,IJwtProvider jwtProvider)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        _jwtProvider = jwtProvider;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {



        var userExists = await _context.Users.AnyAsync(u => u.Email.Trim().ToLower() == dto.Email.Trim().ToLower());
        if (userExists)
        {
            return BadRequest(new { message = "این ایمیل قبلاً در سیستم ثبت شده است." });
        }

        
        var newUser = new User
        {
            Email = dto.Email.Trim().ToLower(),
            PasswordHash =  _passwordHasher.HashPassword(dto.Password),
            Role = "User", 
            IsEmailVerified = false 
        };


        _context.Users.Add(newUser);
        await _context.SaveChangesAsync();
        

        string verificationCode = await _tokenService.GenerateAndSaveEmailVerificationTokenAsync(newUser);

        return Ok(new { 
            message = "ثبت‌نام با موفقیت انجام شد. کد تایید به ایمیل شما ارسال شد (برای تست، کد در همین ریسپانس آمده است).",
            debugCode = verificationCode
        });
    }
    
    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto dto)
    {
        var result = await _tokenService.VerifyEmailTokenAsync(dto.Email, dto.VerificationCode);
    
        if (!result)
        {
            return BadRequest(new { message = "کد وارد شده نامعتبر، منقضی یا تکراری است." });
        }

        return Ok(new { message = "ایمیل شما با موفقیت تایید شد! اکنون می‌توانید لاگین کنید." });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == dto.Email.Trim().ToLower());
        
        if (user == null)
        {
            return Unauthorized(new { message = "ایمیل یا رمز عبور اشتباه است." });
        }
        
        if (!user.IsEmailVerified)
        {
            return BadRequest(new { message = "لطفاً ابتدا ایمیل خود را تایید کنید." });
        }
        
        bool isPasswordValid = _passwordHasher.VerifyPassword(dto.Password, user.PasswordHash);
        if (!isPasswordValid)
        {
            return Unauthorized(new { message = "ایمیل یا رمز عبور اشتباه است." });
        }
        
        string token = _jwtProvider.GenerateToken(user);
        
        return Ok(new { 
            token = token,
            message = "ورود با موفقیت انجام شد." 
        });
    }
    
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == dto.Email.Trim().ToLower());
        

        if (user == null)
        {
            return Ok(new { message = "اگر ایمیل شما در سیستم ثبت شده باشد، کد بازیابی ارسال شد." });
        }

        string resetCode = await _tokenService.GenerateAndSaveResetPasswordTokenAsync(user);

        return Ok(new { 
            message = "کد بازیابی رمز عبور صادر شد (برای تست، کد در همین ریسپانس آمده است).",
            debugCode = resetCode 
        });
    }

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
    {

        var isTokenValid = await _tokenService.VerifyResetPasswordTokenAsync(dto.Email, dto.Code);
        
        if (!isTokenValid)
        {
            return BadRequest(new { message = "کد بازیابی نامعتبر، منقضی شده یا قبلاً استفاده شده است." });
        }


        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.Trim().ToLower() == dto.Email.Trim().ToLower());
        if (user == null)
        {
            return BadRequest(new { message = "کاربر یافت نشد." });
        }


        user.PasswordHash = _passwordHasher.HashPassword(dto.NewPassword);
        await _context.SaveChangesAsync();

        return Ok(new { message = "رمز عبور شما با موفقیت تغییر کرد. اکنون می‌توانید لاگین کنید." });
    }




}