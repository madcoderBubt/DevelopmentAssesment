
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.CQRS;
using ProjectManagement.Infrastracture.DTOs.Auth;
using ProjectManagement.Infrastracture.Services.Interfaces;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly Dispatcher _dispatcher;

        public AuthController(ILogger<AuthController> logger, Dispatcher dispatcher)
        {
            _logger = logger;
            _dispatcher = dispatcher;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GetToken(TokenRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(request);

            return Ok(await _dispatcher.Handle<TokenRequest, TokenDTO>(request));
        }
    }
}
