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

    public IActionResult Index()
    {
        return View(AnimalRepositorys.animals);
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
            return RedirectToAction("Index");
        }
        else
        {
            return Error();
        }
        
    }

    [HttpPost]
    public IActionResult Delete(int? id)
    {
        if (id != null)
        {
            var foundAnimal = AnimalRepositorys.animals.FirstOrDefault(a => a.RegistrationNumber == id);
            if (foundAnimal != null)
            {
                AnimalRepositorys.animals.Remove(foundAnimal);
                Console.WriteLine("Объект Animal удален.");
            }
            else
            {
                Console.WriteLine("Объект Animal не найден.");
            }
            return RedirectToAction("Index");
        }
        return NotFound();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

