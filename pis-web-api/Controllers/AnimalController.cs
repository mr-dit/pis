using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using pis.Models;
using pis.Repositorys;
using pis.Services;
using pis_web_api.Repositorys;

namespace pis.Controllers;


[Route("api/[controller]")]
[ApiController]
    public class AnimalController : ControllerBase
    {
        private readonly ILogger<AnimalController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private AnimalService animalService;

        public AnimalController(ILogger<AnimalController> logger, IWebHostEnvironment appEnvironment)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            animalService = new AnimalService();
        }

        [HttpGet("OpensRegister")]
        public IActionResult OpensRegister(string filterField = "", string filterValue = "", string sortBy = nameof(Animal.AnimalName), bool isAscending = true, int pageNumber = 1, int pageSize = 10)
        {
            var (animals, totalItems) = animalService.GetAnimals(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
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
        public IActionResult AddEntry([FromBody] Animal animal)
        {
            bool status = animalService.FillData(animal);

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
        public IActionResult ChangeEntry(int id, [FromBody] Animal animal)
        {
            if (ModelState.IsValid)
            {
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


        //private readonly ILogger<AnimalController> _logger;


        //IWebHostEnvironment _appEnvironment;

        //public AnimalController(ILogger<AnimalController> logger, IWebHostEnvironment appEnvironment)
        //{
        //    _logger = logger;
        //    _appEnvironment = appEnvironment;

        //}

        //public IActionResult OpensRegister(string filterField, string? filterValue, string sortBy, bool isAscending,
        //    int pageNumber = 1, int pageSize = 10)
        //{
        //    filterValue = filterValue?.ToLower();

        //    var animals = AnimalService.GetAnimals(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
        //    var totalItems = AnimalService.GetTotalAnimals(filterField, filterValue);
        //    var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        //    if (filterValue != null) ViewBag.FilterValue = filterValue;
        //    ViewBag.FilterField = filterField;
        //    ViewBag.SortBy = sortBy;
        //    ViewBag.IsAscending = isAscending;
        //    ViewBag.PageNumber = pageNumber;
        //    ViewBag.PageSize = pageSize;
        //    ViewBag.TotalItems = totalItems;
        //    ViewBag.TotalPages = totalPages;

        //    return View(animals);
        //}


        //public IActionResult AddEntry()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public IActionResult AddEntry(Animal animal)
        //{
        //    bool status = AnimalService.FillData(animal);
        //    if (status)
        //    {
        //        return RedirectToAction("OpensRegister",
        //            new
        //            {
        //                filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
        //                isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
        //            });
        //    }
        //    else
        //    {
        //        return Error();
        //    }
        //}

        //[HttpPost]
        //public IActionResult DeleteEntry(int? id)
        //{
        //    if (id != null)
        //    {
        //        var status = AnimalService.DeleteEntry((int)id);

        //        if (status)
        //        {
        //            Console.WriteLine("Объект Animal удален.");
        //            return RedirectToAction("OpensRegister",
        //                new
        //                {
        //                    filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
        //                    isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
        //                });
        //        }

        //        return Error();
        //    }

        //    return NotFound();
        //}


        //public async Task<IActionResult> ChangeEntry(int? id)
        //{
        //    if (id != null)
        //    {
        //        var animal = AnimalService.GetEntry((int)id);

        //        return View(animal);
        //    }

        //    return NotFound();
        //}

        //[HttpPost]
        //public async Task<IActionResult> ChangeEntry(Animal animal, IFormFile photo)
        //{
        //    //if (photo != null && photo.Length > 0)
        //    //{
        //    //    // путь к папке Files
        //    //    string path = "~/images/" + photo.FileName;
        //    //    // сохраняем файл в папку Files в каталоге wwwroot
        //    //    using (var fileStream = new FileStream(path, FileMode.Create))
        //    //    {
        //    //        await photo.CopyToAsync(fileStream);
        //    //    }
        //    //    animal.Photos = path;
        //    //}

        //    //if (photo != null && photo.Length > 0)
        //    //{
        //    //    // Save the photo to the wwwroot/images directory
        //    //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

        //    //    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

        //    //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    //    {
        //    //        await photo.CopyToAsync(stream);
        //    //    }

        //    //    // Set the photo file name in the animal object
        //    //    animal.Photos = uniqueFileName;

        //    //}


        //    bool status = AnimalService.ChangeEntry(animal);
        //    if (status)
        //    {
        //        return RedirectToAction("OpensRegister",
        //            new
        //            {
        //                filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
        //                isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
        //            });
        //    }

        //    return NotFound();
        //}


        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{
        //    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}
}