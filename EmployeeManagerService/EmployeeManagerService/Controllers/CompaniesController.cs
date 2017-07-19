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
    public class CompaniesController : Controller
    {
        private DatabaseManager dbManager;

        public CompaniesController()
        {
            dbManager = new DatabaseManager();
        }

        public ActionResult Index()
        {
            try
            {
                var companies = dbManager.Companies;
                var companyViewModels = new List<CompanyViewModel>();

                companies.ForEach(company => companyViewModels.Add(company.ToViewModel()));

                return View(companyViewModels);
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
        public ActionResult Create(CompanyViewModel viewModel)
        {
            try
            {
                dbManager.AddCompany(viewModel.ToEntity());
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
                var companyViewModel = dbManager.Companies.Where(com => com.Id == id).FirstOrDefault().ToViewModel();
                ViewBag.Mode = "Изменить";
                return View("Details", companyViewModel);
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult Edit(int id, CompanyViewModel companyViewModel)
        {
            try
            {
                companyViewModel.Id = id;
                dbManager.UpdateCompany(companyViewModel.ToEntity());

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
        }

        public ActionResult Delete(int id)
        {
            try
            {
                dbManager.RemoveCompany(id);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                ViewBag.ErrorMsg = ex.Message;
                return View("Error");
            }
            
        }
    }
}
