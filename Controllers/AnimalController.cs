using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Repositorys;
using pis.Services;

namespace pis.Controllers;

public class AnimalController : Controller
{
    private readonly ILogger<AnimalController> _logger;

    public AnimalController(ILogger<AnimalController> logger)
    {
        _logger = logger;
    }

    public IActionResult OpensRegister(string filterField, string? filterValue, string sortBy, bool isAscending, int pageNumber = 1, int pageSize = 10)
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
            return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });
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
                return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });
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
    public async Task<IActionResult> ChangeEntry(Animal animal)
    {
        
        bool status = AnimalService.ChangeEntry(animal);
        if (status)
        {
            return RedirectToAction("OpensRegister", new { filterField = ViewBag.FilterField, filterValue = ViewBag.FilterValue, sortBy = ViewBag.SortBy, isAscending = ViewBag.IsAscending, pageNumber = ViewBag.PageNumber, pageSize = ViewBag.PageSize });
        }
        else
        {
            return NotFound();
        }
    }
    
    


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

