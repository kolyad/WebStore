using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employee> _employees = new()
        {
            new Employee
            {
                Id = 1,
                LastName = "Иванов",
                FirstName = "Александр",
                Patronymic = "Сергеевич",
                BirthDate = new DateTime(1979, 2, 7),
                HireDate = new DateTime(2010, 5, 15),
                City = "Москва"
            },
            new Employee
            {
                Id = 2,
                LastName = "Васечкин",
                FirstName = "Иван",
                Patronymic = "Васильевич",
                BirthDate = new DateTime(1999, 10, 11),
                HireDate = new DateTime(2015, 1, 20),
                City = "Краснодар"
            },
            new Employee
            {
                Id = 3,
                LastName = "Прохоров",
                FirstName = "Михаил",
                Patronymic = "Фёдорович",
                BirthDate = new DateTime(2001, 1, 21),
                HireDate = new DateTime(2020, 6, 10),
                City = "Пермь"
            }
        };

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employees()
        {
            return View(_employees);
        }

        public IActionResult EmployeeDetail(int Id)
        {
            var model = _employees
                .Where(x => x.Id == Id)
                .FirstOrDefault();

            return View(model);
        }
    }
}
