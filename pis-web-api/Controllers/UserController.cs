using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models.db;
using pis_web_api.Models.post;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private UserService _userService;
        private RoleService _roleService;

        public UserController(ILogger<UserController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _userService = new UserService();
            _roleService = new RoleService();
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string filterValue = "", string filterField = "", string sortBy = nameof(Models.db.User.Surname), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var (users, totalItems) = _userService.GetUsers(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                FilterValue = filterValue,
                FilterField = filterField,
                SortBy = sortBy,
                IsAscending = isAscending,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Users = users
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _userService.GetEntry(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost("addEntry")]
        public IActionResult AddEntry([FromBody] UserPost userPost)
        {
            try
            {
                var user = userPost.ConvertToUser();
                return Ok(user.IdUser);
            }
            catch (Exception)
            {
                return BadRequest("Failed to add organisation entry.");
            }
        }

        [HttpPost("deleteEntry/{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var status = _userService.DeleteEntry(id);

            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest($"Failed to delete organisation entry with ID {id}");
            }
        }

        [HttpPost("changeEntry/{id}")]
        public IActionResult ChangeEntry(int id, [FromBody] UserPost userPost)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = userPost.ConvertToUserWithId(id);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest("Failed to update organisation entry.");
                }
            }

            return BadRequest(ModelState);
        }

        //[HttpPost("addRole")]
        //public IActionResult AddRole(int idUser, int roleId)
        //{
        //    var user = _userService.GetEntry(idUser);
        //    var role =  _roleService.GetEntry(roleId);
        //    var status = user.AddRoles(role);
        //    if(status)
        //        return Ok();
        //    else
        //        return BadRequest("Failed to update organisation entry.");
        //}
    }
}
