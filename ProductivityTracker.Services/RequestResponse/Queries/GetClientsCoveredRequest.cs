namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetClientsCoveredRequest : AuthenticatedRequest
    {
        public string Query { get; set;}
    }
}