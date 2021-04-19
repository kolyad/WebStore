using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IList<Employee> _employees;

        public EmployeesController()
        {
            _employees = TestData.Employees;
        }

        public IActionResult Index()
        {
            return View(_employees);
        }

        public IActionResult Details(int Id)
        {
            var model = _employees.FirstOrDefault(x => x.Id == Id);

            if (model is null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Create() => View("Edit", new Employee());


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var model = _employees.FirstOrDefault(x => x.Id == Id);

            if (model is null)
            {
                return NotFound();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Employee model)
        {
            Employee employee;

            if (model.Id == 0)
            {
                employee = new Employee();
                int maxId = 0;
                if (_employees.Any())
                {
                    maxId = _employees.Max(x => x.Id);
                }
                employee.Id = ++maxId;
                _employees.Add(employee);
            }
            else
            {
                employee = _employees.FirstOrDefault(x => x.Id == model.Id);

                if (employee is null)
                {
                    return NotFound();
                }
            }

            employee.FirstName = model.FirstName;
            employee.LastName = model.LastName;
            employee.Patronymic = model.Patronymic;
            employee.BirthDate = model.BirthDate;
            employee.City = model.City;
            employee.HireDate = model.HireDate;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var model = _employees.FirstOrDefault(x => x.Id == Id);
            if (model is null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(Employee model)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == model.Id);
            if (employee is null)
            {
                return NotFound();
            }

            _employees.Remove(employee);

            return RedirectToAction("Index");
        }
    }
}
