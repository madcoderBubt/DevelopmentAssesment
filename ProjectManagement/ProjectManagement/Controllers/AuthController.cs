using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Infrastracture.DTOs.Auth;
using ProjectManagement.Infrastracture.Services.Interfaces;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IAuthService _authService;

        public AuthController(ILogger<AuthController> logger, IAuthService authService)
        {
            _logger = logger;
            _authService = authService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetToken(TokenRequest request)
        {
            return Ok(await _authService.GetTokenAsync(request.Email, request.Password));
        }
    }
}
