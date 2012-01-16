using System;
using System.Collections.Generic;
using Microsoft.Practices.Prism.ViewModel;

namespace ProductivityTracker.Analyse.Models
{
    public class ProductivityModel : NotificationObject
    {
        public string Id { get; set; }
        public string RecruiterId { get; set; }
        public string RecruiterFullName { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string CandidateId { get; set; }
        public string CandidateName { get; set; }
        public DateTime? DateSent { get; set; }
        public string PositionId { get; set; }
        public string PositionName { get; set; }
        public StatusModel Status { get; set; }
        public string Month { get; set; }
        public string Week { get; set; }
        public DateTime? ExpectedFeedbackDate { get; set; }
        public bool IsFeedbackOverdue { get; set; }
        public IEnumerable<CommentModel> Comments { get; set; }
    }

    public class CommentModel
    {
        public string Description { get; set; }
        public string EnteredBy { get; set; }
    }
}