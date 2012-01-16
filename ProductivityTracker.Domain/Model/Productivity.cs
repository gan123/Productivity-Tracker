using System;
using System.Collections.Generic;

namespace ProductivityTracker.Domain.Model
{
    public class Productivity  : EntityBase
    {
        public Recruiter Recruiter { get; set; }
        public DateTime? DateSent { get; set; }
        public Position Position { get; set; }
        public Client Client { get; set; }
        public Status Status { get; set; }
        public DateTime? ExpectedFeedbackDate { get; set; }
        public Candidate Candidate { get; set; }
        public List<Comment> Comments { get; set; }
    }
}