using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employeesData)
        {
            _employeesData = employeesData;
        }

        public IActionResult Index()
        {
            var employees = _employeesData.Get();

            if (employees.Any())
            {
                var employeesViewModel = employees
                    .Select(x => GetEmployeeViewModel(x))
                    .ToList();

                return View(employeesViewModel);
            }
            else
            {
                return View(new List<EmployeeViewModel>());
            }
        }

        public IActionResult Details(int Id)
        {
            var model = _employeesData.Get(Id);

            if (model is null)
            {
                return NotFound();
            }

            return View(GetEmployeeViewModel(model));
        }

        [HttpGet]
        public IActionResult Create() => View("Edit", new EmployeeViewModel());


        [HttpGet]
        public IActionResult Edit(int Id)
        {
            var model = _employeesData.Get(Id);

            if (model is null)
            {
                return NotFound();
            }

            return View(GetEmployeeViewModel(model));
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel model)
        {
            Employee employee;

            if (model.Id == 0)
            {
                employee = new Employee();
            }
            else
            {
                employee = _employeesData.Get(model.Id);

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

            if (model.Id == 0)
            {
                _employeesData.Add(employee);
            }
            else
            {
                _employeesData.Update(employee);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int Id)
        {
            var employee = _employeesData.Get(Id);
            if (employee is null)
            {
                return NotFound();
            }

            return View(GetEmployeeViewModel(employee));
        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel model)
        {
            var employee = _employeesData.Get(model.Id);
            if (employee is null)
            {
                return NotFound();
            }

            if (_employeesData.Delete(employee.Id))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return NotFound();
            }
        }

        private EmployeeViewModel GetEmployeeViewModel(Employee employee)
        {
            var employeeViewModel = new EmployeeViewModel();
            CopyEmployeeToViewModel(employee, employeeViewModel);            
            return employeeViewModel;
        }

        private void CopyEmployeeToViewModel(Employee employee, EmployeeViewModel model)
        {
            model.Id = employee.Id;
            model.FirstName = employee.FirstName;
            model.LastName = employee.LastName;
            model.Patronymic = employee.Patronymic;
            model.BirthDate = employee.BirthDate;
            model.City = employee.City;
            model.HireDate = employee.HireDate;
        }
    }
}
