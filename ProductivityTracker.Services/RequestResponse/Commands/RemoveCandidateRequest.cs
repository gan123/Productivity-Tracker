namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class RemoveCandidateRequest : AuthenticatedRequest
    {
        public string Id { get; set; }
    }
}