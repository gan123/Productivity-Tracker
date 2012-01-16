using System;
using System.Collections.Generic;

namespace ProductivityTracker.Common
{
    public class ProductivityDto : EntityDto
    {
        public string RecruiterId { get; set; }
        public string RecruiterFullName { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string CandidateId { get; set; }
        public string CandidateName { get; set; }
        public DateTime? DateSent { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public string Month { get; set; }
        public string Week { get; set; }
        public DateTime? ExpectedFeedbackDate { get; set; }
        public List<CommentDto> Comments { get; set; }
        public bool IsFeedbackOverdue
        {
            get
            {
                if (ExpectedFeedbackDate.HasValue)
                    return DateTime.Today > ExpectedFeedbackDate.Value;
                return false;
            }
        }
    }
}