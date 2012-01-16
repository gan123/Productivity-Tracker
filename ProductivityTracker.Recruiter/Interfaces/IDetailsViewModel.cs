using System.Collections.Generic;
using ProductivityTracker.Recruiter.Models;

namespace ProductivityTracker.Recruiter.Interfaces
{
    public interface IDetailsViewModel
    {
        IEnumerable<SummaryModel> Summary { get; set; }
        void Load(string id);
    }
}