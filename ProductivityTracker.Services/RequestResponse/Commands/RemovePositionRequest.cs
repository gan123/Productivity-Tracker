namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class RemovePositionRequest : AuthenticatedRequest
    {
        public string Id { get; set; }
    }
}