using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.Infrastracture.DTOs.UserDTOs.Queries;

namespace ProjectManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        //private readonly

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {

            return Ok();
        }
    }
}
