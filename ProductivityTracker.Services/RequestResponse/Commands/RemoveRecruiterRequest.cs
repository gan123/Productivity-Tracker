namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class RemoveRecruiterRequest : AuthenticatedRequest
    {
        public string RecruiterId { get; set; }
    }
}