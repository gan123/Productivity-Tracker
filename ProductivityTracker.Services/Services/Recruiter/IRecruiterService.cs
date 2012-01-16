using System;
using System.Collections.Generic;

namespace ProductivityTracker.Services.Services.Recruiter
{
    public interface IRecruiterService
    {
        void UpdatePassword(string login, string password);
        void Remove(string id);
        void Add(string fullName, string email, DateTime? dateOfBirth, DateTime dateOfJoining, string contact, string designation, bool isAdmin);
        void Update(string id, string fullName, string email, string contact, string designation);

        IEnumerable<Domain.Model.Recruiter> GetRecruiters();
        IEnumerable<Domain.Model.PositionCovered> GetPositionsCovered(string recruiterId);
    }
}