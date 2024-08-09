using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EmployeeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
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
