using Agatha.Common;

namespace ProductivityTracker.Services.RequestResponse.Queries
{
    public class GetPositionsCoveredRequest : Request
    {
        public string RecruiterId { get; set; }
    }
}