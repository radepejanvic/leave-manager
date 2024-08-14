using Core.Services.IService;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Models.Models;
using Models.ViewModels;
using Utils;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class LeaveRequestController : Controller
    {
        private readonly ILogger<LeaveRequestController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILeaveRequestService _leaveRequestService;

        public LeaveRequestController(ILogger<LeaveRequestController> logger, IUnitOfWork unitOfWork, ILeaveRequestService leaveRequestService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _leaveRequestService = leaveRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            var leaveTypes = SD.LeaveTypes
               .Select(u => new SelectListItem { Text = u, Value = u});

            var leaveRequestVM = new LeaveRequestVM
            {
                LeaveRequest = new LeaveRequest(),
                Types = leaveTypes
            };

            if (id == null || id == 0)
            {
                return View(leaveRequestVM);
            }
            else
            {
                leaveRequestVM.LeaveRequest = _unitOfWork.LeaveRequest.Get(u => u.Id == id);
                return View(leaveRequestVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(LeaveRequestVM leaveRequestVM)
        {
            if (!ModelState.IsValid)
            {
                return View(leaveRequestVM);
            }

            var leaveRequest = leaveRequestVM.LeaveRequest;

            if (_leaveRequestService.IsOverlapping(leaveRequest)) { 
                TempData["error"] = "Leave Request overlaps with the existing one.";

                leaveRequestVM.Types = SD.LeaveTypes
                    .Select(u => new SelectListItem { Text = u, Value = u });
                return View(leaveRequestVM);
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
