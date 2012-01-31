using Agatha.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class SearchProductivitiesRequest : Request
    {
        public string ClientId { get; set; }
        public string CandidateId { get; set; }
        public string StatusId { get; set; }
        public string Month { get; set; }
        public string Week { get; set; }
        public string RecruiterId { get; set; }
    }
}