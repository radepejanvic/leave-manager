using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Org.BouncyCastle.Crypto.Prng.Drbg;
using System.Net.NetworkInformation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
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

            if (string.IsNullOrEmpty(email))
            {
                return View(new Employee() { Action = CRUDAction.ADD });

            }
            else
            {
                Employee employee = _unitOfWork.Employee.Get(u => u.Email == email);
                employee.Action = CRUDAction.UPDATE;

                return View(employee);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Employee employee)
        {
            var modelState = ModelState;
            Console.WriteLine($"Errors: {ModelState.ErrorCount}");
            Console.WriteLine($"Errors: {ModelState}");
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
                return View(employee);
            }
        }

        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Employee> employees = _unitOfWork.Employee.GetAll().ToList();
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
