namespace Core.Services.IService
{
    public interface IPeriodicTaskService
    {
        Task StartAsync(Func<Task> action, int interval, CancellationToken cancellationToken);
        void Stop();
    }
}
