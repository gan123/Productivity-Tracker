namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class RemoveIndustryRequest : AuthenticatedRequest
    {
        public string Id { get; set; }
    }
}