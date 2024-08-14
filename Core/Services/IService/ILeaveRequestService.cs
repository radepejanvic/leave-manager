using Models.Models;

namespace Core.Services.IService
{
    public interface ILeaveRequestService
    {
        Task<int> ExtractLeaveRequestsAsync();

        bool IsOverlapping(LeaveRequest leaveRequest); 
    }
}
