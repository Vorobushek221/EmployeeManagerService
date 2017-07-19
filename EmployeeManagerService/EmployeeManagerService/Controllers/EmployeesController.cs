using EmployeeManagerService.Models;
using EmployeeManagerService.Models.Entities;
using EmployeeManagerService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeManagerService.Controllers
{
    public class EmployeesController : Controller
    {
        private DatabaseManager dbManager;

        public EmployeesController()
        {
            dbManager = new DatabaseManager();
        }

        public ActionResult Index()
        {
            try
            {
                var employees = dbManager.Employees;
                var employeeViewModels = new List<EmployeeViewModel>();

                employees.ForEach(emp => employeeViewModels.Add(emp.ToViewModel()));

                return View(employeeViewModels);
            }

            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
        }

        public ActionResult Create()
        {
            ViewBag.Mode = "Добавить";
            return View("Details");
        }

        [HttpPost]
        public ActionResult Create(EmployeeViewModel employeeViewModel)
        {
            try
            {
                dbManager.AddEmployee(employeeViewModel.ToEntity());

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
        }

        public ActionResult Edit(int id)
        {
            try
            {
                var employeeViewModel = dbManager.Employees.Where(emp => emp.Id == id).FirstOrDefault().ToViewModel();
                return View("Details", employeeViewModel);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, EmployeeViewModel employeeViewModel)
        {
            try
            {
                dbManager.UpdateEmployee(employeeViewModel.ToEntity());

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                dbManager.RemoveEmployee(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
        }

        public static List<SelectListItem> FillCompaniesDropdown()
        {
            var list = new List<SelectListItem>();
            var companies = new DatabaseManager().Companies;
            foreach (var com in companies)
            {
                list.Add(new SelectListItem() { Text = com.Name, Value = com.Id.ToString() });
            }
            return list;
        }
    }
}

