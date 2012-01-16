namespace ProductivityTracker.Services.Services.Authroisation
{
    public interface ICurrentUser
    {
        string Login { get; set; }
    }

    public class CurrentUser : ICurrentUser
    {
        public string Login { get; set; }
    }
}