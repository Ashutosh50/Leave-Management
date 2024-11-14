using LeaveManagement.Data;
using LeaveManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeaveManagement.Controllers
{
    public class LeaveReqController : Controller
    {
        private readonly LeaveDataAccessLayer _dal;
        public LeaveReqController(LeaveDataAccessLayer dal)
        {
            _dal = dal;
        }
        public IActionResult AddLeaveReq()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLeaveReq(LeaveReq req)
        {
            try
            {
                _dal.AddLeaveReq(req);
                return RedirectToAction("GetAllRequests");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult GetAllRequests()
        {
            List<LeaveReq> emps = _dal.GetLeaveReqs(); 
            return View(emps);
        }

        public IActionResult UpdateStatus(int id,string status) 
        {
            try
            {
                _dal.UpdateStatus(id, status);
                return RedirectToAction("GetAllRequests");
            }
            catch (Exception ex) 
            {
                return View();
            }
        }

        public IActionResult GetReqByEmpId(int id) 
        {
            LeaveReq le = _dal.GetReqById(id);
            return View(id);
        }

    }
}
