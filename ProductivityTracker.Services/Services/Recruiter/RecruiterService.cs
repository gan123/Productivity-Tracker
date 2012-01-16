using System;
using System.Collections.Generic;
using System.Linq;
using ProductivityTracker.Domain.Model;

namespace ProductivityTracker.Services.Services.Recruiter
{
    public class RecruiterService : ServiceBase, IRecruiterService
    {
        public void UpdatePassword(string login, string password)
        {
            var recruiter = CurrentSession.Query<Domain.Model.Recruiter>().Single(r => r.Login == login);
            recruiter.Password = password;
        }

        public void Remove(string id)
        {
            var recruiter = CurrentSession.Load<Domain.Model.Recruiter>(id);
            CurrentSession.Delete(recruiter);
        }

        public void Add(string fullName, string email, DateTime? dateOfBirth, DateTime dateOfJoining, string contact, string designation, bool isAdmin)
        {
            var recruiter = new Domain.Model.Recruiter
                                {
                                    FullName = fullName,
                                    Email = email,
                                    Designation = designation,
                                    DateOfBirth = dateOfBirth,
                                    Contact = contact,
                                    Login = fullName.Split(' ')[0].ToLower(),
                                    Password = "password",
                                    IsAdmin = isAdmin
                                };
            CurrentSession.Store(recruiter);
        }

        public void Update(string id, string fullName, string email, string contact, string designation)
        {
            var recruiter = CurrentSession.Load<Domain.Model.Recruiter>(id);
            recruiter.FullName = fullName;
            recruiter.Email = email;
            recruiter.Contact = contact;
            recruiter.Designation = designation;
        }

        public IEnumerable<Domain.Model.Recruiter> GetRecruiters()
        {
            return CurrentSession.Query<Domain.Model.Recruiter>();
        }

        public IEnumerable<Domain.Model.PositionCovered> GetPositionsCovered(string recruiterId)
        {
            var recruiter = CurrentSession.Load<Domain.Model.Recruiter>(recruiterId);
            return CurrentSession.Query<Domain.Model.Productivity>().Where(p => p.Recruiter.Id == recruiter.Id).Select(p => new Domain.Model.PositionCovered {Client = p.Client, Id = p.Position.Id, Name = p.Position.Name});
        }
    }
}