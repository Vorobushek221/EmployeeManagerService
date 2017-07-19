using EmployeeManagerService.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagerService.Models.ViewModels
{
    public class CompanyViewModel
    {
        [DisplayName("№")]
        public int Id { get; set; }

        [DisplayName("Имя")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Размер")]
        [Required]
        public int Size { get; set; }

        [DisplayName("Организационно-правовая форма")]
        [Required]
        public string LegalForm { get; set; }

        public Company ToEntity()
        {
            return new Company
            {
                Id = this.Id,
                Name = this.Name,
                LegalForm = this.LegalForm,
                Size = this.Size
            };
        }
    }
}