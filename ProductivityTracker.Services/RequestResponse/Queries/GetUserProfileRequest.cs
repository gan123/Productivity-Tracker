using Agatha.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetUserProfileRequest : Request
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}