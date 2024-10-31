using Backend2.DTO;
using Backend2.Model;
using Backend2.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Backend2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterDTO registerDTO)
        {
            var result = await _authService.RegisterAsync(registerDTO);
            return Ok(result); 
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDTO loginDTO)
        {
            var token = await _authService.LoginAsync(loginDTO);
            return Ok(new { Token = token });
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> GetAllUsers()
        {
            var users = await _authService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsersById(int id)
        {
            var users = await _authService.GetUsersByIdAsync(id);
            if (users == null)
                return NotFound();

            return Ok(users);
        }
    }
}
