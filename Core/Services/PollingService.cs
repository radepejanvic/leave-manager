using Core.Services.IService;
using static System.Environment;

namespace Core.Services
{
    public class PollingService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly int _pollingDelay;

        public PollingService(ILeaveRequestService leaveRequestService, IConfiguration configuration)
        {
            _leaveRequestService = leaveRequestService;
            _pollingDelay = 6000;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async (e) =>
            {
                await _leaveRequestService.ExtractLeaveRequestsAsync();
            }, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(_pollingDelay));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
