using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.CQRS;
using ProjectManagement.Infrastracture.DTOs.UserDTOs.Queries;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly Dispatcher _dispatcher;

        public UserController(Dispatcher dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var x = await _dispatcher.Handle<GetUserByIdRequest, GetUserResponse>(new GetUserByIdRequest() { UserId = id });
            return Ok(x);
        }
    }
}
