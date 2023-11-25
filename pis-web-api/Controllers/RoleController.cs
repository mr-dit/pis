using Microsoft.AspNetCore.Mvc;
using pis_web_api.Models.db;
using pis_web_api.Services;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : Controller
    {
        private readonly ILogger<RoleController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private RoleService _roleService;

        public RoleController(ILogger<RoleController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _roleService = new RoleService();
        }

        [HttpGet("opensRegister")]
        public IActionResult OpensRegister(string filterValue = "", int pageNumber = 1, int pageSize = 100)
        {
            var (roles, totalItems) = _roleService.GetRoles(filterValue, pageNumber, pageSize);
            var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var result = new
            {
                FilterValue = filterValue,
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalItems = totalItems,
                TotalPages = totalPages,
                Roles = roles
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetRole(int id)
        {
            var organisation = _roleService.GetEntry(id);

            if (organisation == null)
            {
                return NotFound();
            }

            return Ok(organisation);
        }

        //[HttpPost("addEntry")]
        //public IActionResult AddEntry([FromBody] Role role)
        //{
        //    bool status = _roleService.AddEntry(role);

        //    if (status)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest("Failed to add organisation entry.");
        //    }
        //}

        //[HttpPost("deleteEntry/{id}")]
        //public IActionResult DeleteEntry(int id)
        //{
        //    var status = _roleService.DeleteEntry(id);

        //    if (status)
        //    {
        //        return Ok();
        //    }
        //    else
        //    {
        //        return BadRequest($"Failed to delete organisation entry with ID {id}");
        //    }
        //}

        //[HttpPost("changeEntry/{id}")]
        //public IActionResult ChangeEntry(int id, [FromBody] Role role)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bool status = _roleService.ChangeEntry(role);

        //        if (status)
        //        {
        //            return Ok();
        //        }
        //        else
        //        {
        //            return BadRequest("Failed to update organisation entry.");
        //        }
        //    }

        //    return BadRequest(ModelState);
        //}
    }
}
