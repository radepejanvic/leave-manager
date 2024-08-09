using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using static System.Net.WebRequestMethods;
using static System.Environment;
using Azure.AI.OpenAI;
using Azure;
using OpenAI.Chat;
using Core.Services.IService;
using Core.Services;
using Models.Models;
using DataAccess.Repository.IRepository;

namespace Core.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ParserController : Controller
    {
        private readonly ILogger<ParserController> _logger;
        private readonly ILeaveRequestService _leaveRequestService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly string endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        private readonly string key = GetEnvironmentVariable("AZURE_OPENAI_KEY");
        private readonly string model = GetEnvironmentVariable("AZURE_OPENAI_MODEL");

        public ParserController(ILogger<ParserController> logger, IUnitOfWork unitOfWork, ILeaveRequestService leaveRequestService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _leaveRequestService = leaveRequestService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
            return Json(new
            {
                Emails = _unitOfWork.Email.GetUnreadMailsAsync()
            });
        }

        [HttpGet]
        public async Task<IActionResult> AIEmailIntegration()
        {
            await _leaveRequestService.ExtractLeaveRequestsAsync();
            
            return Json(new
            {
                Called = "AIEmailIntegration called"
            });
        }

    }
}
