using EmployeeManagerService.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagerService.Models.Entities
{
    public class Company
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Size { get; set; }

        public string LegalForm { get; set; }

        public CompanyViewModel ToViewModel()
        {
            return new CompanyViewModel
            {
                Id = this.Id,
                Name = this.Name,
                LegalForm = this.LegalForm,
                Size = this.Size
            };
    }
    }
}