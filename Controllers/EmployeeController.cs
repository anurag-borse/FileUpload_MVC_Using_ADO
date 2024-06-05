using FileUpload_MVC_ADO.Data;
using FileUpload_MVC_ADO.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FileUpload_MVC_ADO.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            EmployeeRepository employeeRepository = new EmployeeRepository();

            employees = employeeRepository.GetEmployees();
            return View(employees);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Employee employee, HttpPostedFileBase file)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    EmployeeRepository employeeRepository = new EmployeeRepository();


                    if (file != null)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                        file.SaveAs(_path);

                        employee.ImagePath = "/Images/" + _FileName;
                    }



                    if (employeeRepository.AddEmployee(employee))
                    {
                        return RedirectToAction("Index");
                    }

                }
                return View();
            }
            catch
            {
                return View();
            }


        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            Employee employees = new Employee();
            EmployeeRepository employeeRepository = new EmployeeRepository();

            employees = employeeRepository.GetEmployeesByID(id);
            return View(employees);
        }


        [HttpPost]
        public ActionResult Edit(int id, Employee employee, HttpPostedFileBase file)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        var employeeInDb = new EmployeeRepository().GetEmployeesByID(id);

                        if (employeeInDb.ImagePath != null)
                        {
                            string fullPath = Request.MapPath("~" + employeeInDb.ImagePath);
                            if (System.IO.File.Exists(fullPath))
                            {
                                System.IO.File.Delete(fullPath);
                            }

                            string _FileName = Path.GetFileName(file.FileName);
                            string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                            file.SaveAs(_path);

                            employee.ImagePath = "/Images/" + _FileName;

                        }

                    }
                    else
                    {
                        var employeeInDb = new EmployeeRepository().GetEmployeesByID(id);
                        employee.ImagePath = employeeInDb.ImagePath;
                    }


                    EmployeeRepository employeeRepository = new EmployeeRepository();

                    if (employeeRepository.UpdateEmployee(id, employee))
                    {
                        return RedirectToAction("Index");
                    }

                }
                return View();
            }
            catch
            {
                return View();
            }
        }


        [HttpGet]
        public ActionResult Delete(int id)
        {
            Employee employees = new Employee();
            EmployeeRepository employeeRepository = new EmployeeRepository();

            employees = employeeRepository.GetEmployeesByID(id);
            return View(employees);
        }



        [HttpPost]
        public ActionResult Delete(Employee employee)
        {
            try
            {
                EmployeeRepository employeeRepository = new EmployeeRepository();

                var employeeInDb = new EmployeeRepository().GetEmployeesByID(employee.ID);
                if(employeeInDb.ImagePath != null)
                {
                    string fullPath = Request.MapPath("~" + employeeInDb.ImagePath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }


                if (employeeRepository.DeleteEmployee(employee.ID))
                {
                    return RedirectToAction("Index");
                }
                return View();
            }
            catch
            {
                return View();
            }



        }




    }
}