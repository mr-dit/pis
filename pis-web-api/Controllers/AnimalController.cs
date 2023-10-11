using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using pis_web_api.Models;
using pis_web_api.Services;
using System.Diagnostics;

namespace pis_web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly ILogger<AnimalController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;

        public AnimalController(ILogger<AnimalController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
        }

        [HttpGet("OpensRegister")]
        public IActionResult OpensRegister(string? filterValue, string? sortBy, bool isAscending, string filterField = nameof(Animal.RegistrationNumber), int pageNumber = 1, int pageSize = 10)
        {
            filterValue = filterValue?.ToLower();

            var animals = AnimalService.GetAnimals(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
            var totalItems = AnimalService.GetTotalAnimals(filterField, filterValue);
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
            var animal = AnimalService.GetEntry(id);

            if (animal == null)
            {
                return NotFound();
            }

            return Ok(animal);
        }

        [HttpPost("AddEntry")]
        public IActionResult AddEntry([FromBody] Animal animal)
        {
            bool status = AnimalService.FillData(animal);

            if (status)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Failed to add animal entry.");
            }
        }

        [HttpPost("DeleteEntry/{id}")]
        public IActionResult DeleteEntry(int id)
        {
            var status = AnimalService.DeleteEntry(id);

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
        public IActionResult ChangeEntry(int id, [FromBody] Animal animal)
        {
            var existingAnimal = AnimalService.GetEntry(id);

            if (existingAnimal == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                bool status = AnimalService.ChangeEntry(existingAnimal);

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
