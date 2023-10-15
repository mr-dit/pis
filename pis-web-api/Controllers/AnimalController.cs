using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Repositorys;
using pis.Services;

namespace pis.Controllers;

public class AnimalController : Controller
{
    private readonly ILogger<AnimalController> _logger;

    
    IWebHostEnvironment _appEnvironment;

    public AnimalController(ILogger<AnimalController> logger, IWebHostEnvironment appEnvironment)
    {
        _logger = logger;
        _appEnvironment = appEnvironment;

    }

    public IActionResult OpensRegister(string filterField, string? filterValue, string sortBy, bool isAscending,
        int pageNumber = 1, int pageSize = 10)
    {
        filterValue = filterValue?.ToLower();

        var animals = AnimalService.GetAnimals(filterField, filterValue, sortBy, isAscending, pageNumber, pageSize);
        var totalItems = AnimalService.GetTotalAnimals(filterField, filterValue);
        var totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

        if (filterValue != null) ViewBag.FilterValue = filterValue;
        ViewBag.FilterField = filterField;
        ViewBag.SortBy = sortBy;
        ViewBag.IsAscending = isAscending;
        ViewBag.PageNumber = pageNumber;
        ViewBag.PageSize = pageSize;
        ViewBag.TotalItems = totalItems;
        ViewBag.TotalPages = totalPages;

        return View(animals);
    }


    public IActionResult AddEntry()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddEntry(Animal animal)
    {
        bool status = AnimalService.FillData(animal);
        if (status)
        {
            return RedirectToAction("OpensRegister",
                new
                {
                    filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
                    isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
                });
        }
        else
        {
            return Error();
        }
    }

    [HttpPost]
    public IActionResult DeleteEntry(int? id)
    {
        if (id != null)
        {
            var status = AnimalService.DeleteEntry((int)id);

            if (status)
            {
                Console.WriteLine("Объект Animal удален.");
                return RedirectToAction("OpensRegister",
                    new
                    {
                        filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
                        isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
                    });
            }

            return Error();
        }

        return NotFound();
    }


    public async Task<IActionResult> ChangeEntry(int? id)
    {
        if (id != null)
        {
            var animal = AnimalService.GetEntry((int)id);

            return View(animal);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> ChangeEntry(Animal animal, IFormFile photo)
    {
        //if (photo != null && photo.Length > 0)
        //{
        //    // путь к папке Files
        //    string path = "~/images/" + photo.FileName;
        //    // сохраняем файл в папку Files в каталоге wwwroot
        //    using (var fileStream = new FileStream(path, FileMode.Create))
        //    {
        //        await photo.CopyToAsync(fileStream);
        //    }
        //    animal.Photos = path;
        //}

        //if (photo != null && photo.Length > 0)
        //{
        //    // Save the photo to the wwwroot/images directory
        //    string uniqueFileName = Guid.NewGuid().ToString() + "_" + photo.FileName;

        //    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", uniqueFileName);

        //    using (var stream = new FileStream(filePath, FileMode.Create))
        //    {
        //        await photo.CopyToAsync(stream);
        //    }

        //    // Set the photo file name in the animal object
        //    animal.Photos = uniqueFileName;

        //}


        bool status = AnimalService.ChangeEntry(animal);
        if (status)
        {
            return RedirectToAction("OpensRegister",
                new
                {
                    filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy,
                    isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize
                });
        }

        return NotFound();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}