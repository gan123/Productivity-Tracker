using System;

namespace ProductivityTracker.Common
{
    public class RecruiterDto : UserDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Contact { get; set; }
        public string Designation { get; set; }
        public DateTime DateOfJoining { get; set; }
        public bool IsAdmin { get; set; }
    }
}

