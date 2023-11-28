using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using NUnit.Framework;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Models.db;
using pis_web_api.Models.get;
using pis_web_api.Models.post;
using pis_web_api.Repositorys;
using pis_web_api.Services;

namespace pis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly ILogger<AnimalController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private AnimalService animalService;
        private JournalService journalService;

        public AnimalController(ILogger<AnimalController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            animalService = new AnimalService();
            journalService = new JournalService();
        }

        [HttpPost("OpensRegister")]
        public IActionResult OpensRegister([FromBody]UserPost user, string filterField = "", string filterValue = "", string sortBy = nameof(Animal.AnimalName), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            List<Animal> animals;
            int totalItems;

            if (user.Roles.Intersect(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 12, 13, 14, 15}).Count() != 0)
            {
                (animals, totalItems) = animalService.GetAnimals(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            }
            else if (user.Roles.Intersect(new List<int>() { 9, 10, 11 }).Count() != 0)
            {
                (animals, totalItems) = animalService.GetAnimalsByOrg(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize, user);
            }
            else
            {
                return Forbid();
            }
            
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
                Animals = animals
            };

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAnimal(int id)
        {
            var animal = animalService.GetEntry(id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        [HttpPost("AddEntry")]
        public IActionResult AddEntry([FromBody] AnimalPost animalPost, int userId)
        {
            if (ModelState.IsValid)
            {
                var animal = animalPost.ConvertToAnimal();

                bool status = animalService.AddEntry(animal);

                if (status)
                {
                    journalService.JournalAddAnimal(userId, animal.RegistrationNumber);
                    return Ok(animal.RegistrationNumber);
                }
                else
                {
                    return BadRequest("Failed to add animal entry.");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("DeleteEntry/{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var status = animalService.DeleteEntry(id);

            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest($"Failed to delete animal entry with ID {id}");
            }
        }

        [HttpPost("ChangeEntry/{id}")]
        public IActionResult ChangeEntry(int id, [FromBody] AnimalPost animalPost)
        {
            if (ModelState.IsValid)
            {
                var animal = animalPost.ConvertToAnimalWithId(id);
                bool status = animalService.ChangeEntry(animal);

                if (status)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Failed to update animal entry.");
                }
            }

            return BadRequest(ModelState);
        }
    }
}