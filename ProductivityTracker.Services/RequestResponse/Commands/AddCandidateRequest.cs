namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class AddCandidateRequest : AuthenticatedRequest
    {
        public string Name { get; set; }
        public string Company { get; set; }
        public string Contact { get; set; }
        public string CurrentCtc { get; set; }
        public string ExpectedCtc { get; set; }
        public string NoticePeriod { get; set; }
        public string Position { get; set; }
    }
}