namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class AddPositionRequest : AuthenticatedRequest
    {
        public string Name { get; set; }
    }
}