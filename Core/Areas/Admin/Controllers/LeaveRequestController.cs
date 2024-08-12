using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LeaveRequestController : Controller
    {
        private readonly ILogger<LeaveRequestController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public LeaveRequestController(ILogger<LeaveRequestController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            if (id == null || id == 0)
            {
                return View(new LeaveRequest());
            }
            else
            {
                LeaveRequest leaveRequest = _unitOfWork.LeaveRequest.Get(u => u.Id == id);
                return View(leaveRequest);
            }
        }

        [HttpPost]
        public IActionResult Upsert(LeaveRequest leaveRequest)
        {
            if (!ModelState.IsValid)
            {
                return View(leaveRequest);
            }

            var existingLeaveRequest = _unitOfWork.LeaveRequest
                .Get(u => u.EmployeeEmail == leaveRequest.EmployeeEmail &&
                u.Id != leaveRequest.Id && 
                ((u.Start >= leaveRequest.Start && u.Start <= leaveRequest.End) ||
                (u.End >= leaveRequest.Start && u.End <= leaveRequest.End))
                );

            if (existingLeaveRequest != null) { 
                TempData["error"] = "Leave Request overlaps with the existing one.";
                return View(leaveRequest);
            }

            if (leaveRequest.Id == 0)
            {
                _unitOfWork.LeaveRequest.Add(leaveRequest);
            }
            else
            {
                _unitOfWork.LeaveRequest.Update(leaveRequest);
            }

            _unitOfWork.Save();
            TempData["success"] = "Leave Request upserted successfully.";
            return RedirectToAction("Index");
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<LeaveRequest> leaveRequests = _unitOfWork.LeaveRequest.GetAll().ToList();
            return Json(new { data = leaveRequests });
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var leaveRequest = _unitOfWork.LeaveRequest.Get(u => u.Id == id);

            if (leaveRequest == null)
            {
                return Json(new { success = false, message = "Error while deleting Leave Request." });
            }

            _unitOfWork.LeaveRequest.Remove(leaveRequest);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful." });
        }
        #endregion
    }
}
