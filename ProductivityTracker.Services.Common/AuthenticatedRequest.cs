using Agatha.Common;

namespace ProductivityTracker.Services.Common
{
    public class AuthenticatedRequest : Request
    {
        public string CurrentLoggedInUserLogin { get; set; }
    }
}