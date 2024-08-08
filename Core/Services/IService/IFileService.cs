namespace Core.Services.IService
{
    public interface IFileService
    {
        string? Read();
        void Write(string value);
        void Append(string line);
    }
}
