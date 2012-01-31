using System;
using System.Collections.Generic;

namespace ProductivityTracker.Domain.Model
{
    public class FlatProductivity : EntityBase
    {
        public DenormalizedReference Recruiter { get; set; }
        public DateTime? DateSent { get; set; }
        public DenormalizedReference Position { get; set; }
        public DenormalizedReference Client { get; set; }
        public DenormalizedReference Status { get; set; }
        public DateTime? ExpectedFeedbackDate { get; set; }
        public List<Comment> Comments { get; set; }
        public DenormalizedReference Candidate { get; set; }
        public string Month { get; set; }
        public string Week { get; set; }
    }

    public class DenormalizedReference
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}