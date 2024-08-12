using Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Models.Models;
using Core.Services.IService;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILeaveRequestService _leaveRequestService;

        public HomeController(ILogger<HomeController> logger, ILeaveRequestService leaveRequestService)
        {
            _logger = logger;
            _leaveRequestService = leaveRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #region API CALLS
        [HttpPost]
        public async Task<IActionResult> PollEmails()
        {
            var emailCount = await _leaveRequestService.ExtractLeaveRequestsAsync();
            TempData["success"] = $"Succesfully polled {emailCount} new emails.";

            return Json(new
            {
                emailCount
            });
        }
        #endregion
    }
}
