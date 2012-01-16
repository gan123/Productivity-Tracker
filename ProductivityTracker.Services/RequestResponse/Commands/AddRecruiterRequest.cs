using System;

namespace ProductivityTracker.Services.RequestResponse.Commands
{
    public class AddRecruiterRequest : AuthenticatedRequest
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Contact { get; set; }
        public string Designation { get; set; }
        public bool IsAdmin { get; set; }
    }
}