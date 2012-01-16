namespace ProductivityTracker.Services.Services.Authroisation
{
    public interface IAuthorisationService
    {
        Domain.Model.Recruiter GetUserProfile(string login, string password);
    }
}