using System;
using System.Collections.Generic;
using System.Linq;
using ProductivityTracker.Services.Services.Client;
using ProductivityTracker.Services.Services.Helpers;

namespace ProductivityTracker.Services.Services.Candidate
{
    public class CandidateService : ServiceBase, ICandidateService
    {
        public IEnumerable<Domain.Model.Candidate> GetAll()
        {
            return CurrentSession.Query<Domain.Model.Candidate>();
        }

        public IEnumerable<Domain.Model.Candidate> Find(string query)
        {
            return CurrentSession.Advanced.LuceneQuery<Domain.Model.Candidate>("CandidateIndex")
                .WhereContains("Name", QueryExtensions.CleanseAndSplit(query)).ToList();
        }

        public void Add(string name, string contact, string company, string position, string currentCtc, string expectedCtc, string notice)
        {
            var candidate = new Domain.Model.Candidate
                                {
                                    Company = company,
                                    Contact = contact,
                                    CurrentCtc = currentCtc,
                                    ExpectedCtc = expectedCtc,
                                    Name = name,
                                    NoticePeriod = notice,
                                    Position = position
                                };
            CurrentSession.Store(candidate);
        }

        public void Remove(string id)
        {
            var candidate = CurrentSession.Load<Domain.Model.Candidate>(id);
            CurrentSession.Delete(candidate);
        }

        public void Update(string id, string name, string contact, string company, string position, string currentCtc, string expectedCtc, string notice)
        {
            var candidate = CurrentSession.Load<Domain.Model.Candidate>(id);
            candidate.Company = company;
            candidate.Contact = contact;
            candidate.CurrentCtc = currentCtc;
            candidate.ExpectedCtc = expectedCtc;
            candidate.Name = name;
            candidate.NoticePeriod = notice;
            candidate.Position = position;
        }
    }
}