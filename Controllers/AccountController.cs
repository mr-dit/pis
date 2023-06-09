using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pis.Models;
using pis.Repositorys;
using pis.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace pis.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        public AccountController(ILogger<AccountController> logger)
        {
            _logger = logger;
        }

        public IActionResult Login()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Authenticate(Login model)
        {
            // Проверка введенных данных пользователя и аутентификация
            if (model.Username == "user1" && model.Password == "pipa")
            {
                // Успешная аутентификация
                // Сохранение сессии пользователя
                ViewBag.HideMenu = false;
                HttpContext.Session.SetString("Username", model.Username);
                return RedirectToAction("OpensRegister", "Animal");
            }
            else
            {
                // Неверные данные пользователя, отображение сообщения об ошибке
                ViewBag.HideMenu = true;
                ModelState.AddModelError(string.Empty, "Неверный логин или пароль");
                return View("Login", model);
            }
        }
    }
}

