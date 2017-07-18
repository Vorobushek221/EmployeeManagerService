using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeManagerService.Models
{
    public class Employee
    {
        public int Id { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string FatherName { get; set; }

        public DateTime RecruitDate { get; set; }

        public Rank Rank { get; set; }

        public Company Company { get; set; }
    }
}