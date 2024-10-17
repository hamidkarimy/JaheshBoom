using JaheshBoom.Services;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace JaheshBoom.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;

        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            // ارسال درخواست به سرویس احراز هویت
            var result = await _authService.Login(loginRequest.AppKey, loginRequest.AppSecretKey);
                  return Ok(new { Token = result });  // نتیجه موفقیت‌آمیز به همراه توکن
        }
    }

    // مدل برای درخواست لاگین
    public class LoginRequest
    {
        public string AppKey { get; set; }
        public string AppSecretKey { get; set; }
    }
}
