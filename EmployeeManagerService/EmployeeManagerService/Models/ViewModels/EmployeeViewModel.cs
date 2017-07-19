using EmployeeManagerService.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagerService.Models.ViewModels
{
    public class EmployeeViewModel
    {
        [DisplayName("№")]
        public int Id { get; set; }

        [DisplayName("Фамилия")]
        [Required]
        public string Surname { get; set; }

        [DisplayName("Имя")]
        [Required]
        public string Name { get; set; }

        [DisplayName("Отчество")]
        [Required]
        public string FatherName { get; set; }

        [DisplayName("Дата приема на работу")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required]
        public DateTime RecruitDate { get; set; }

        [DisplayName("Должность")]
        [Required]
        public Rank Rank { get; set; }

        [DisplayName("Компания")]
        [Required]
        public int CompanyId { get; set; }

        [DisplayName("Компания")]
        [Required]
        public string CompanyName { get; set; }

        public Employee ToEntity()
        {
            return new Employee
            {
                Id = this.Id,
                Surname = this.Surname,
                Name = this.Name,
                FatherName = this.FatherName,
                Rank = (int)this.Rank,
                RecruitDate = this.RecruitDate,
                CompanyId = CompanyId
            };
        }
    }
}