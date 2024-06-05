using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FileUpload_MVC_ADO.Models
{
    public class Employee
    {
        [Key]
        [Display(Name = "Employee ID")]
        public int ID { get; set; }

        [Display(Name = "Employee Name")]
        public string Name { get; set; }

        public string ImagePath { get; set; }


        [Display(Name = "Employee Email")]
        public string Email { get; set; }



        [Display(Name = "Employee Department")]
        public string Department { get; set; }

    }
}