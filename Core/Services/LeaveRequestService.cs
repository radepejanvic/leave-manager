using Azure.AI.OpenAI;
using Azure;
using Core.Services.IService;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OpenAI.Chat;
using static System.Environment;
using System.Text;
using Models.Models;
using System.Text.Json;
using Utils;

namespace Core.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPeriodicTaskService _periodicTaskService;
        private readonly ChatClient _chatClient;
        private readonly SystemChatMessage _systemPrompt;

        public LeaveRequestService(IUnitOfWork unitOfWork, IPeriodicTaskService periodicTaskService)
        {
            _unitOfWork = unitOfWork;
            _chatClient = CreateClient();
            _systemPrompt = CreateSystemPrompt();
            _periodicTaskService = periodicTaskService;
        }

        public async Task<int> ExtractLeaveRequestsAsync()
        {
            var emails = await _unitOfWork.Email.PollUnreadMailsAsync();

            foreach (var email in emails)
            {
                ChatCompletion completion = _chatClient.CompleteChat(
                    [
                        _systemPrompt,
                        new UserChatMessage(email.ToString()),
                    ]);

                var jsonString = completion.Content[0].Text;
                _unitOfWork.LeaveRequest.Add(JsonSerializer.Deserialize<LeaveRequest>(jsonString));
            }
            _unitOfWork.Save();

            return emails.Count();
        }

        private ChatClient CreateClient()
        {
            var endpoint = GetEnvironmentVariable("AZURE_OPENAI_ENDPOINT");
            var key = GetEnvironmentVariable("AZURE_OPENAI_KEY");
            var model = GetEnvironmentVariable("AZURE_OPENAI_MODEL");
            
            AzureOpenAIClient azureClient = new(
                new Uri(endpoint),
                new AzureKeyCredential(key));

            return azureClient.GetChatClient(model);
        }

        private SystemChatMessage? CreateSystemPrompt()
        {
            var systemPromptFile = GetEnvironmentVariable("SYSTEM_PROMPT_FILE");
            try
            {
                using StreamReader reader = new(systemPromptFile, Encoding.UTF8);
                var content = reader.ReadToEnd();
                return new SystemChatMessage(content);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error while reading System Prompt file: {ex.Message}");
            }
            return null;
        }
    }
}
