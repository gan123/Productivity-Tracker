using System.Collections.Generic;

namespace ProductivityTracker.Services.Services.Candidate
{
    public interface ICandidateService
    {
        IEnumerable<Domain.Model.Candidate> GetAll();
        IEnumerable<Domain.Model.Candidate> Find(string query);
        void Add(string name, string contact, string company, string currentCtc, string expectedCtc, string notice);
        void Remove(string id);
        void Update(string id, string name, string contact, string company, string currentCtc, string expectedCtc, string notice);
    }
}