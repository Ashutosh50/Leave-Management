using LeaveManagement.Data;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDataAccessLayer dal;

        //public EmployeeController() 
        //{
        //    dal = new EmployeeDataAccessLayer();
        //}
        public EmployeeController(EmployeeDataAccessLayer _dal) 
        {
            dal = _dal;
        }

        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Employees emp)
        {
            try
            {
                dal.AddEmployee(emp);
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
        public IActionResult GetAll()
        {
            List<Employees> emps = dal.GetAllEmp();
            return View(emps);
        }

        public IActionResult Edit(int id) 
        {
            Employees emp = dal.GetById(id);
            return View(emp);
        }
        [HttpPost]
        public IActionResult Edit(Employees emp)
        {
            try
            {
                dal.UpdateEmployee(emp);
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult Details(int id)
        {
            Employees emp = dal.GetById(id);
            return View(emp);
        }
        public IActionResult Delete(int id)
        {
            Employees emp = dal.GetById(id);
            return View(emp);
        }
        [HttpPost]
        public IActionResult Delete(Employees emp)
        {
            try
            {
                dal.Delete(emp.EmployeeId);
                return RedirectToAction("GetAll");
            }
            catch (Exception ex)
            {
                return View();
            }
        }
    }
}
