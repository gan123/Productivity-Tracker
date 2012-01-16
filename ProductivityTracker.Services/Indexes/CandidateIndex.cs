using System.Linq;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace ProductivityTracker.Services.Indexes
{
    public class CandidateIndex : AbstractIndexCreationTask<Domain.Model.Candidate>
    {
        public CandidateIndex()
        {
            Map = candidates => from r in candidates
                                select new { r.Name };

            Index(r => r.Name, FieldIndexing.Analyzed);
        }
    }
}