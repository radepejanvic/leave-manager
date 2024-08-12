namespace Core.Services.IService
{
    public interface ILeaveRequestService
    {
        Task<int> ExtractLeaveRequestsAsync();
    }
}
