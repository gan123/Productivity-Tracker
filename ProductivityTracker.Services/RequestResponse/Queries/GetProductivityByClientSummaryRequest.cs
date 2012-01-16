using Agatha.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetProductivityByClientSummaryRequest : Request
    {
        public string ClientId { get; set; }
    }
}