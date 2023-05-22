using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Repositorys;

namespace pis.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(AnimalRepositorys.animals);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Animal animal)
    {
        AnimalRepositorys.animals.Add(animal);
        return RedirectToAction("Index");
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



    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

