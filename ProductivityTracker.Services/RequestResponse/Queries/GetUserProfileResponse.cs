using Agatha.Common;
using ProductivityTracker.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetUserProfileResponse : Response
    {
        public RecruiterDto Recruiter { get; set; }
    }
}