using Microsoft.AspNetCore.Mvc;
using StudentManagementApp.DTOs;
using StudentManagementApp.Models;
using StudentManagementApp.Services;
using StudentManagementApp.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IConfiguration _config;

        private readonly EmailService _emailService;

public AuthController(UserService userService, IConfiguration config, EmailService emailService)
{
    _userService = userService;
    _config = config;
    _emailService = emailService;
}

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var existingUser = await _userService.GetUserByEmailAsync(dto.Email);
            if (existingUser != null)
                return BadRequest("User already exists");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                IsVerified = false
            };

            await _userService.CreateUserAsync(user);
            return Ok("User registered");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var user = await _userService.GetUserByEmailAsync(dto.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid credentials");

            if (!user.IsVerified)
                return Unauthorized("Please verify OTP first");

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["TokenKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id),
                    new Claim(ClaimTypes.Name, user.Email)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string jwt = tokenHandler.WriteToken(token);

            return Ok(new { token = jwt });
        }

        [HttpPost("send-otp")]
public async Task<IActionResult> SendOtp([FromBody] string email)
{
    var user = await _userService.GetUserByEmailAsync(email);
    if (user == null)
        return NotFound("User not found");

    string otp = new Random().Next(100000, 999999).ToString();
    user.OtpCode = otp;
    user.OtpExpiry = DateTime.UtcNow.AddMinutes(5);
    await _userService.UpdateUserAsync(user.Id!, user);

    string subject = "Your OTP Code";
    string body = $"Hi {user.Username},\n\nYour OTP is: {otp}\nIt will expire in 5 minutes.";

    await _emailService.SendEmailAsync(email, subject, body);

    return Ok("OTP sent to your email.");
}


        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] OtpVerifyDto dto)
        {
            var user = await _userService.GetUserByEmailAsync(dto.Email);
            if (user == null)
                return NotFound("User not found");

            if (user.OtpCode != dto.Otp || user.OtpExpiry < DateTime.UtcNow)
                return BadRequest("Invalid or expired OTP");

            user.IsVerified = true;
            user.OtpCode = null;
            user.OtpExpiry = null;
            await _userService.UpdateUserAsync(user.Id!, user);

            return Ok("OTP verified successfully.");
        }
    }
}
