using Agatha.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetCandidatesRequest : Request
    {
        public string Query { get; set; }
    }
}