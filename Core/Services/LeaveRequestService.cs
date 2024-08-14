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
        private readonly ChatClient _chatClient;
        private readonly SystemChatMessage _systemPrompt;

        public LeaveRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _chatClient = CreateClient();
            _systemPrompt = CreateSystemPrompt();
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
                var leaveRequest = JsonSerializer.Deserialize<LeaveRequest>(jsonString);
                if(!IsOverlapping(leaveRequest))
                {
                    _unitOfWork.LeaveRequest.Add(leaveRequest);
                }
            }
            _unitOfWork.Save();

            return emails.Count();
        }

        public bool IsOverlapping(LeaveRequest leaveRequest)
        {
            var existingLeaveRequest = _unitOfWork.LeaveRequest
                .Get(u => u.EmployeeEmail == leaveRequest.EmployeeEmail &&
                u.Id != leaveRequest.Id &&
                ((u.Start >= leaveRequest.Start && u.Start <= leaveRequest.End) ||
                (u.End >= leaveRequest.Start && u.End <= leaveRequest.End))
                );
            
            return existingLeaveRequest != null;
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
