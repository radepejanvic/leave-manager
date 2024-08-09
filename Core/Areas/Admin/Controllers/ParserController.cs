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
        private readonly IFileService _fileService;

        private readonly IUnitOfWork _unitOfWork;

        private readonly string endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
        private readonly string key = GetEnvironmentVariable("AZURE_OPENAI_KEY");
        private readonly string model = GetEnvironmentVariable("AZURE_OPENAI_MODEL");

        public ParserController(ILogger<ParserController> logger, IFileService fileService, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetResponse([FromBody] Email email)
        {
            AzureOpenAIClient azureClient = new(
                new Uri(endpoint),
                new AzureKeyCredential(key));

            ChatClient chatClient = azureClient.GetChatClient(model);

            ChatCompletion completion = chatClient.CompleteChat(
                [
                    new SystemChatMessage(_fileService.Read()),
                    new UserChatMessage(email.ToString()),
                ]);

            Console.WriteLine($"{completion.Role}: {completion.Content[0].Text}");


            return Json(new
            {
                Role = completion.Role.ToString(),
                Response = completion.Content[0].Text
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetEmails()
        {
            return Json(new
            {
                Emails = _unitOfWork.Email.GetUnreadMails()
            });
        }

    }
}
