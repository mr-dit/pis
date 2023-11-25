using Microsoft.AspNetCore.Mvc;
using pis.Controllers;
using pis.Services;
using pis_web_api.Models.post;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthControllercs : ControllerBase
    {
        private readonly ILogger<OrganisationController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private UserService _userService;
        private RoleService _roleService;

        public AuthControllercs(ILogger<OrganisationController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _userService = new UserService();
            _roleService = new RoleService();
        }

        [HttpPost("login")]
        public IActionResult Login(string login, string password)
        {
            try
            {
                var user = _userService.LoginUser(login, password);
                var userPost = new UserPost();
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
