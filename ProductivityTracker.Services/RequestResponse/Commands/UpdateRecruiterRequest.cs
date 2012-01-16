namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class UpdateRecruiterRequest : AuthenticatedRequest
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Contact { get; set; }
        public string Designation { get; set; }
    }
}