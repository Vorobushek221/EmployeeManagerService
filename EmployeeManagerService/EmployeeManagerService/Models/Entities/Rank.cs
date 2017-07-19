using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeManagerService.Models.Entities
{
    public enum Rank
    {
        [Display(Name = "Разработчик")]
        Developer,

        [Display(Name = "Тестировщик")]
        Tester,

        [Display(Name = "Бизнес-аналитик")]
        BusinessAnalyst,

        [Display(Name = "Менеджер")]
        Manager
    }
}