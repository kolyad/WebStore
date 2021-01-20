using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee { Id = 1, LastName = "Иванов", FirstName = "Александр", Patronymic = "Сергеевич", Age = 20 },
            new Employee { Id = 2, LastName = "Васечкин", FirstName = "Иван", Patronymic = "Васильевич", Age = 35 },
            new Employee { Id = 3, LastName = "Прохоров", FirstName = "Михаил", Patronymic = "Фёдорович", Age = 63 }
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View(_employees);
        }
    }
}
