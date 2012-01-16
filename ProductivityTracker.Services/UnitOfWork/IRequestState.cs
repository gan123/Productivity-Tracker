namespace ProductivityTracker.Services.UnitOfWork
{
    public interface IRequestState
    {
        T Get<T>(string key);
        void Store(string key, object something);
    }
}