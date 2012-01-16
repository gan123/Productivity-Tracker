namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class UpdatePasswordRequest : AuthenticatedRequest
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}