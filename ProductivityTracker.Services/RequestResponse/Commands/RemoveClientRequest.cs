namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class RemoveClientRequest : AuthenticatedRequest
    {
        public string Id { get; set; }
    }
}