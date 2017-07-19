using EmployeeManagerService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace EmployeeManagerService.Models.Entities
{
    public class Employee
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string FatherName { get; set; }

        public DateTime RecruitDate { get; set; }

        public int Rank { get; set; }

        public int CompanyId { get; set; }

        public EmployeeViewModel ToViewModel()
        {
            var employeeViewModel = new EmployeeViewModel
            {
                Id = this.Id,
                Surname = this.Surname,
                Name = this.Name,
                FatherName = this.FatherName,
                RecruitDate = this.RecruitDate,
                Rank = (Rank)this.Rank,
                CompanyId = this.CompanyId
            };

            var companies = new DatabaseManager().Companies;
            var company = companies.Where(com => com.Id == this.CompanyId).ToList().FirstOrDefault().ToViewModel();

            employeeViewModel.CompanyName = company.Name;

            return employeeViewModel;
        }
    }
}