using Agatha.Common;

namespace ProductivityTracker.Services.RequestResponse
{
    public class AuthenticatedRequest : Request
    {
        public string CurrentUserLogin { get; set; }
    }
}