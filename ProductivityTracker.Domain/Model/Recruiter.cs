using System;

namespace ProductivityTracker.Domain.Model
{
    public class Recruiter : User
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public int? Age
        {
            get
            {
                if (DateOfBirth.HasValue)
                    return DateTime.Now.Year - DateOfBirth.Value.Year;
                return null;
            }
        }
        public DateTime? DateOfBirth { get; set; }
        public DateTime DateOfJoining { get; set; }
        public string Contact { get; set; }
        public string Designation { get; set; }
        public bool IsAdmin { get; set; }
    }
}
