using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Models.Models;
using Models.ViewModels;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using System.Net.NetworkInformation;
using Utils;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(ILogger<EmployeeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(string? email)
        {
            var employeeVM = new EmployeeVM();

            if (string.IsNullOrEmpty(email))
            {
                employeeVM.Employee = new Employee() { Action = CRUDAction.ADD };
                return View(employeeVM);
            }
            else
            {
                employeeVM.Employee = _unitOfWork.Employee.Get(u => u.Email == email);
                employeeVM.Employee.Action = CRUDAction.UPDATE;
                employeeVM.TotalVacationDays = _unitOfWork.LeaveRequest.GetAll(u => u.EmployeeEmail == email && u.Type == SD.Vacation).Sum(u => u.Duration);
                employeeVM.TotalRemoteDays = _unitOfWork.LeaveRequest.GetAll(u => u.EmployeeEmail == email && u.Type == SD.Remote).Sum(u => u.Duration);
                employeeVM.TotalSickDays = _unitOfWork.LeaveRequest.GetAll(u => u.EmployeeEmail == email && u.Type == SD.SickLeave).Sum(u => u.Duration);
                return View(employeeVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(EmployeeVM employeeVM)
        {
            var employee = employeeVM.Employee;
            if (ModelState.IsValid)
            {
                if (employee.Action == CRUDAction.ADD)
                {
                    _unitOfWork.Employee.Add(employee);
                }
                else
                {
                    _unitOfWork.Employee.Update(employee);
                }

                _unitOfWork.Save();
                TempData["success"] = "Employee upserted successfully.";
                return RedirectToAction("Index");
            }
            else
            {
                return View(employeeVM);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var employees = _unitOfWork.Employee.GetAll()
            .Select(e => new EmployeeVM
            {
                Employee = e,
                TotalVacationDays = _unitOfWork.LeaveRequest.GetAll(u => u.EmployeeEmail == e.Email && u.Type == SD.Vacation).Sum(u => u.Duration), 
                TotalRemoteDays = _unitOfWork.LeaveRequest.GetAll(u => u.EmployeeEmail == e.Email && u.Type == SD.Remote).Sum(u => u.Duration),
                TotalSickDays = _unitOfWork.LeaveRequest.GetAll(u => u.EmployeeEmail == e.Email && u.Type == SD.SickLeave).Sum(u => u.Duration)
            }).ToList();

            return Json(new { data = employees });
        }


        [HttpDelete]
        public IActionResult Delete(string? email)
        {
            var employee = _unitOfWork.Employee.Get(u => u.Email == email);
            
            if (employee == null)
            {
                return Json(new { success = false, message = "Error while deleting employee." });
            }
            
            _unitOfWork.Employee.Remove(employee);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete Successful." });
        }
        #endregion
    }
}
