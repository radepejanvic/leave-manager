using Core.Services.IService;
using Microsoft.VisualBasic;

namespace Core.Services
{
    public class PeriodicTaskService : IPeriodicTaskService
    {
        private CancellationTokenSource _cancellationTokenSource;

        public PeriodicTaskService()
        {
        }

        public async Task StartAsync(Func<Task> action, int interval, CancellationToken cancellationToken)
        {
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            var counter = 0;
            try
            {
                while (!_cancellationTokenSource.Token.IsCancellationRequested)
                {
                    Console.WriteLine($"Periodic Task [{counter}]");
                    await action();
                    await Task.Delay(interval, _cancellationTokenSource.Token);
                    counter++;
                }
            }
            catch (TaskCanceledException)
            {
                Console.WriteLine("Task was cancelled due to an exception.");
            }
        }

        public void Stop()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
