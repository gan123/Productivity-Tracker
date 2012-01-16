using System.Collections.Generic;

namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class AddClientRequest : AuthenticatedRequest
    {
        public string Name { get; set; }
        public string IndustryId { get; set; }
        public string Website { get; set; }
        public string Contacts { get; set; }
        public string Address { get; set; }
        public List<string> PositionIds { get; set; }
    }
}