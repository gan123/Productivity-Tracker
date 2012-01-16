namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class AddIndustryRequest : AuthenticatedRequest
    {
        public string Name { get; set; }
    }
}