namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class UpdateStatusAndCommentsRequest : AuthenticatedRequest
    {
        public string Id { get; set; }
        public string StatusId { get; set; }
        public string StatusName { get; set; }
        public string Comments { get; set; }
    }
}