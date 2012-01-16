using System;

namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class AddProductivityRequest : AuthenticatedRequest
    {
        public string RecruiterId { get; set; }
        public string RecruiterName { get; set; }
        public DateTime? DateSent { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime? ExpectedFeedbackDate { get; set; }
        public string Comments { get; set; }
        public string CandidateId { get; set; }
        public string CandidateName { get; set; }
    }
}