using Core.Services.IService;
using System.Text;
using static System.Environment;

namespace Core.Services
{
    public class FileService : IFileService
    {
        private readonly string? _systemPrompt;
        private readonly string? _output;

        public FileService()
        {
            _systemPrompt = GetEnvironmentVariable("SYSTEM_PROMPT_FILE");
            _output = GetEnvironmentVariable("OUTPUT_FILE");
        }

        public void Append(string line)
        {
            throw new NotImplementedException();
        }

        public string? Read()
        {
            try
            {
                using (StreamReader reader = new StreamReader(_systemPrompt, Encoding.UTF8))
                {
                    var content = reader.ReadToEnd();
                    return content;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while reading System Propmt file: {e.Message}");
            }
            return null;
        }

        public void Write(string value)
        {
            throw new NotImplementedException();
        }
    }
}
